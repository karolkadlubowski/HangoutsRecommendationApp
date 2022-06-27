import axios from 'axios';
import { ErrorMessage, Field, Form, Formik } from 'formik';
import { useState } from 'react';
import toast from 'react-hot-toast';
import * as Yup from 'yup';
import Header from '../Header';

export function CreatePlace() {
    const token = localStorage.getItem('token');
    const [files, setFiles] = useState<FileList | null>(null);

    const createVenue = (
        VenueName: string,
        Description: string,
        CategoryName: string,
        Address: string,
        Latitude: string,
        Longitude: string,
        Style: number,
        Occupancy: number,
        Photos: FileList | string
    ) => {
        let bodyFormData = new FormData();

        bodyFormData.append('VenueName', VenueName);
        bodyFormData.append('Description', Description);
        bodyFormData.append('CategoryName', CategoryName);
        bodyFormData.append('Address', Address);
        bodyFormData.append('Latitude', Latitude);
        bodyFormData.append('Longitude', Longitude);
        bodyFormData.append('Style', String(Style));
        bodyFormData.append('Occupancy', String(Occupancy));
        const PhotosLength = Photos.length;
        for (let i = 0; i < PhotosLength; i++) {
            bodyFormData.append('Photos', Photos[i]);
        }

        axios({
            method: 'post',
            url: 'http://localhost:8000/api/v1/Venue',
            data: bodyFormData,
            headers: { 'Content-Type': 'multipart/form-data', 'Authorization': `Bearer ${token}` },
        })
            .then(function (response) {
                console.log('success', response);
                toast.success('Successfully added venue');
            })
            .catch(function (response) {
                console.log('error', response);
                toast.error('Something went wrong');
            });
    };

    const validationSchema = Yup.object().shape({
        venueName: Yup.string().required(),
        description: Yup.string().required(),
        categoryName: Yup.string().required(),
        address: Yup.string().required(),
        latitude: Yup.number().required(),
        longitude: Yup.number().required(),
        style: Yup.number(),
        occupancy: Yup.number(),
        photos: Yup.mixed<FileList>(),
    });

    const onFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setFiles(event.target.files);
    };

    return (
        <>
            <Header />
            <Formik
                initialValues={{
                    venueName: '',
                    description: '',
                    categoryName: '',
                    address: '',
                    latitude: '',
                    longitude: '',
                    style: 0,
                    occupancy: 0,
                    photos: new Blob(),
                }}
                validationSchema={validationSchema}
                onSubmit={(data) => {
                    console.log('testdata', data, files);
                    createVenue(
                        data.venueName,
                        data.description,
                        data.categoryName,
                        data.address,
                        data.latitude,
                        data.longitude,
                        data.style,
                        data.occupancy,
                        files ?? ''
                    );
                }}
            >
                {(formikProps) => (
                    <div className="flex justify-center items-center bg-gray-100 page-height">
                        <div className="w-full flex justify-center mt-14">
                            <Form className="bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4">
                                <div>
                                    <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="venueName">
                                        Venue Name
                                    </label>
                                    <Field
                                        className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                        id="venueName"
                                        placeholder={'Venue Name'}
                                        name="venueName"
                                    />
                                    <p className="mt-1 text-red-500 text-xs italic">
                                        <ErrorMessage name="venueName" />
                                    </p>
                                </div>
                                <div className="mt-4">
                                    <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="description">
                                        Description
                                    </label>
                                    <Field
                                        className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                        id="description"
                                        placeholder={'Description'}
                                        name="description"
                                    />
                                    <p className="mt-1 text-red-500 text-xs italic">
                                        <ErrorMessage name="description" />
                                    </p>
                                </div>
                                <div className="mt-4">
                                    <label
                                        className="block text-gray-700 text-sm font-bold mb-2"
                                        htmlFor="categoryName"
                                    >
                                        Category name
                                    </label>
                                    <Field
                                        className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                        id="categoryName"
                                        placeholder={'Category name'}
                                        name="categoryName"
                                    />
                                    <p className="mt-1 text-red-500 text-xs italic">
                                        <ErrorMessage name="categoryName" />
                                    </p>
                                </div>
                                <div className="mt-4">
                                    <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="address">
                                        Address
                                    </label>
                                    <Field
                                        className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                        id="address"
                                        placeholder={'Address'}
                                        name="address"
                                    />
                                    <p className="mt-1 text-red-500 text-xs italic">
                                        <ErrorMessage name="address" />
                                    </p>
                                </div>
                                <div className="mt-4">
                                    <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="latitude">
                                        Latitude
                                    </label>
                                    <Field
                                        className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                        id="latitude"
                                        placeholder={'Latitude'}
                                        name="latitude"
                                    />
                                    <p className="mt-1 mt-1 text-red-500 text-xs italic">
                                        <ErrorMessage name="latitude" />
                                    </p>
                                </div>
                                <div className="mt-4">
                                    <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="longitude">
                                        Longitude
                                    </label>
                                    <Field
                                        className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                        id="longitude"
                                        placeholder={'Longitude'}
                                        name="longitude"
                                    />
                                    <p className="mt-1 text-red-500 text-xs italic">
                                        <ErrorMessage name="longitude" />
                                    </p>
                                </div>

                                <div className="mt-4 ">
                                    <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="style">
                                        Style
                                    </label>
                                    <div className="inline-block relative w-full">
                                        <Field
                                            as="select"
                                            className="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline"
                                            id="style"
                                            name="style"
                                        >
                                            <option key={0} value={0} label={'Casual'}>
                                                Casual
                                            </option>
                                            <option key={1} value={1} label={'Retro'}>
                                                Retro
                                            </option>
                                            <option key={2} value={2} label={'Modern'}>
                                                Modern
                                            </option>
                                        </Field>
                                        <div className="pointer-events-none absolute inset-y-0 right-0 flex items-center px-2 text-gray-700">
                                            <svg
                                                className="fill-current h-4 w-4"
                                                xmlns="http://www.w3.org/2000/svg"
                                                viewBox="0 0 20 20"
                                            >
                                                <path d="M9.293 12.95l.707.707L15.657 8l-1.414-1.414L10 10.828 5.757 6.586 4.343 8z" />
                                            </svg>
                                        </div>
                                    </div>
                                </div>

                                <div className="mt-4 ">
                                    <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="occupancy">
                                        Occupancy
                                    </label>
                                    <div className="inline-block relative w-full">
                                        <Field
                                            as="select"
                                            className="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline"
                                            id="occupancy"
                                            name="occupancy"
                                        >
                                            <option key={0} value={0} label={'Low'}>
                                                Low
                                            </option>
                                            <option key={1} value={1} label={'Medium'}>
                                                Medium
                                            </option>
                                            <option key={2} value={2} label={'High'}>
                                                High
                                            </option>
                                        </Field>
                                        <div className="pointer-events-none absolute inset-y-0 right-0 flex items-center px-2 text-gray-700">
                                            <svg
                                                className="fill-current h-4 w-4"
                                                xmlns="http://www.w3.org/2000/svg"
                                                viewBox="0 0 20 20"
                                            >
                                                <path d="M9.293 12.95l.707.707L15.657 8l-1.414-1.414L10 10.828 5.757 6.586 4.343 8z" />
                                            </svg>
                                        </div>
                                    </div>
                                </div>

                                <div className="mt-4">
                                    <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="photos">
                                        Photos
                                    </label>
                                    <input
                                        className="appearance-none w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                        id="photos"
                                        placeholder={'Photos'}
                                        name="photos"
                                        type="file"
                                        multiple
                                        onChange={onFileChange}
                                    />
                                    <div className="mt-5">
                                        {/* {files ? <img src={} alt="" width="400" height="auto" /> : null} */}
                                    </div>
                                </div>
                                <div className="flex items-center justify-between mt-4">
                                    <button
                                        className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
                                        type="submit"
                                    >
                                        Create
                                    </button>
                                </div>
                            </Form>
                        </div>
                    </div>
                )}
            </Formik>
        </>
    );
}
