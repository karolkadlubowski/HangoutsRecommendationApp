import axios from 'axios';
import { ErrorMessage, Field, Form, Formik } from 'formik';
import { useState } from 'react';
import * as Yup from 'yup';
import Header from '../Header';

export function CreatePlace() {
    const [files, setFiles] = useState<string>('');
    const [imgUrl, setImgUrl] = useState<any>();

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
        venueName: Yup.string().required(),
        description: Yup.string().required(),
        categoryName: Yup.string().required(),
        address: Yup.string().required(),
        latitude: Yup.number().required(),
        longitude: Yup.number().required(),
        photos: Yup.mixed<Blob>(),
    });

    const onFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        if (event.target.files) {
            const fileUrl = URL.createObjectURL(event.target.files[0]);
            setFiles(fileUrl);
        }
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
                        files
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
                                    name="venueName"
                                />
                                <div className="text-rose-600">
                                    <ErrorMessage name="venueName" />
                                </div>
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
                                <div className="text-rose-600">
                                    <ErrorMessage name="description" />
                                </div>
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
                                <div className="text-rose-600">
                                    <ErrorMessage name="categoryName" />
                                </div>
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
                                <div className="text-rose-600">
                                    <ErrorMessage name="address" />
                                </div>
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
                                <div className="text-rose-600">
                                    <ErrorMessage name="latitude" />
                                </div>
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
                                <div className="text-rose-600">
                                    <ErrorMessage name="longitude" />
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
                                    <img src={files} alt="" width="400" height="auto" />
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
