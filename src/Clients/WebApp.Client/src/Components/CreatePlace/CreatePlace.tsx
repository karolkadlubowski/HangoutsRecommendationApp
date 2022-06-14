import axios from 'axios';
import { Field, Form, Formik } from 'formik';
import { useState } from 'react';
import * as Yup from 'yup';
import Header from '../Header';

export function CreatePlace() {
    const [files, setFiles] = useState<Blob | null>(null);

    const createVenue = (
        VenueName: string,
        Description: string,
        CategoryName: string,
        Address: string,
        Latitude: string,
        Longitude: string,
        Photos: Blob | string
    ) => {
        let bodyFormData = new FormData();

        bodyFormData.append('VenueName', VenueName);
        bodyFormData.append('Description', Description);
        bodyFormData.append('CategoryName', CategoryName);
        bodyFormData.append('Address', Address);
        bodyFormData.append('Latitude', Latitude);
        bodyFormData.append('Longitude', Longitude);
        bodyFormData.append('Photos', Photos);

        axios({
            method: 'post',
            url: 'http://localhost:8000/api/v1/Venue',
            data: bodyFormData,
            headers: { 'Content-Type': 'multipart/form-data' },
        })
            .then(function (response) {
                console.log('success', response);
            })
            .catch(function (response) {
                console.log('error', response);
            });
    };

    const validationSchema = Yup.object().shape({
        venueName: Yup.string(),
        description: Yup.string(),
        categoryName: Yup.string(),
        address: Yup.string(),
        latitude: Yup.number(),
        longitude: Yup.number(),
        photos: Yup.mixed<Blob>(),
    });

    const onFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setFiles(event.target.files ? event.target.files[0] : null);
        console.log(files instanceof Blob);
    };

    return (
        <>
            <Header />
            <Formik
                initialValues={{
                    'venue name': '',
                    'description': '',
                    'categoryName': '',
                    'address': '',
                    'latitude': '',
                    'longitude': '',
                    'photos': new Blob(),
                }}
                validationSchema={validationSchema}
                onSubmit={(data) => {
                    console.log('testdata', data, files);
                    createVenue(
                        data['venue name'],
                        data.description,
                        data.categoryName,
                        data.address,
                        data.latitude,
                        data.longitude,
                        files ?? ''
                    );
                }}
            >
                {(formikProps) => (
                    <div className="w-full flex justify-center mt-10">
                        <Form className="bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4">
                            <div>
                                <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="venueName">
                                    Venue Name
                                </label>
                                <Field
                                    className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                    id="venueName"
                                    placeholder={'Venue Name'}
                                    name="venue name"
                                />
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
                            </div>
                            <div className="mt-4">
                                <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="categoryName">
                                    Category name
                                </label>
                                <Field
                                    className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                    id="categoryName"
                                    placeholder={'Category name'}
                                    name="categoryName"
                                />
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
                )}
            </Formik>
        </>
    );
}
