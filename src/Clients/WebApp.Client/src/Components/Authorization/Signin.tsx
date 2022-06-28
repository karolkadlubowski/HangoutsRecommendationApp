import axios from 'axios';
import { ErrorMessage, Field, Form, Formik } from 'formik';
import toast from 'react-hot-toast';
import { Link, useNavigate } from 'react-router-dom';
import * as Yup from 'yup';

export default function Signup() {
    const navigate = useNavigate();
    const validationSchema = Yup.object().shape({
        email: Yup.string().email().required(),
        password: Yup.string().required(),
    });

    const Signup = (Email: string, Password: string) => {
        axios({
            method: 'post',
            url: 'http://localhost:8010/api/v1/Identity/signin',
            data: {
                email: Email,
                password: Password,
            },
            headers: { 'Content-Type': 'application/json' },
        })
            .then(function (response) {
                localStorage.setItem('token', response.data.jwtToken);
                navigate('/', { replace: true });
            })
            .catch(function (response) {
                toast.error('Wrong login details.');
            });
    };

    return (
        <>
            <Formik
                initialValues={{
                    email: '',
                    password: '',
                }}
                validationSchema={validationSchema}
                onSubmit={(data) => {
                    Signup(data.email, data.password);
                    console.log(data);
                }}
            >
                {(formikProps) => (
                    <div className="bg-slate-100 h-screen">
                        <div className="flex justify-center items-center" style={{ height: '80vh' }}>
                            <div className="w-full p-2 max-w-md">
                                <Form className="bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4">
                                    <h1 className="block text-gray-700 text-2xl mb-6">Sign in</h1>
                                    <div className="mb-4">
                                        <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="email">
                                            Email
                                        </label>
                                        <Field
                                            className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                            id="email"
                                            placeholder={'Email'}
                                            name="email"
                                        />
                                        <p className="mt-1 text-red-500 text-xs italic">
                                            <ErrorMessage name="email" />
                                        </p>
                                    </div>
                                    <div className="mb-6">
                                        <label
                                            className="block text-gray-700 text-sm font-bold mb-2"
                                            htmlFor="password"
                                        >
                                            Password
                                        </label>
                                        <Field
                                            className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 mb-3 leading-tight focus:outline-none focus:shadow-outline"
                                            id="password"
                                            name="password"
                                            type="password"
                                            placeholder={'******************'}
                                        />
                                        <p className="mt-1 text-red-500 text-xs italic">
                                            <ErrorMessage name="password" />
                                        </p>
                                    </div>
                                    <div className="flex items-center justify-between">
                                        <button
                                            className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
                                            type="submit"
                                        >
                                            Sign In
                                        </button>
                                        <Link
                                            to={'/signup'}
                                            className="inline-block align-baseline font-bold text-sm text-blue-500 hover:text-blue-800"
                                        >
                                            Create an account
                                        </Link>
                                    </div>
                                </Form>
                                <p className="text-center text-gray-500 text-xs">&copy;2022 Projekt TAB</p>
                            </div>
                        </div>
                    </div>
                )}
            </Formik>
        </>
    );
}
