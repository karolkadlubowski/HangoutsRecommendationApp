import { Field, Form, Formik } from 'formik';
import * as Yup from 'yup';
import Header from '../Header';

function CreatePlace() {

    const yupFile = Yup
		.mixed<File>()
		// I don't think it can be anything else, so let's leave it not translated
		.test('isAFile', 'the input must be a file', (value) => value instanceof File)
		.test(
			'fileSize',
			'file too large',
			(file?: File) => (file?.size ?? Infinity) < 10
		)
		.required();

    const validationSchema = Yup.object().shape({
		venueName: Yup.string(),
		Description: Yup.string(),
		CategoryName: Yup.string(),
		Address: Yup.string(),
		Latitude: Yup.string(),
		Longitude: Yup.string(),
        Photos: Yup.array(yupFile)
        .max(
            10,
            'too many files limit'
        ),
	});

  return (
      <>
      <Header/>
      <Formik
         initialValues={{
            venueName: '',
            Description: '',
            CategoryName: '',
            Address: '',
            Latitude: '',
            Longitude: '',
            Photos: ''
        }}
        validationSchema={validationSchema}
        onSubmit={() => {}}>
				{(formikProps) => (
                    <div className="w-full flex justify-center mt-10">
                        <Form className="bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4">
                            <div>
                                <label className='block text-gray-700 text-sm font-bold mb-2' htmlFor="venueName">
                                    Venue Name
                                </label>
                                <Field
                                    className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                    id="venueName"
                                    placeholder={'Venue Name'}
                                    name="venue name"
                                />
                            </div>
                            <div className='mt-4'>
                                <label className='block text-gray-700 text-sm font-bold mb-2' htmlFor="description">
                                    Description
                                </label>
                                <Field
                                    className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                    id="description"
                                    placeholder={'Description'}
                                    name="description"
                                />
                            </div>
                            <div className='mt-4'>
                                <label className='block text-gray-700 text-sm font-bold mb-2' htmlFor="categoryName">
                                    Category name
                                </label>
                                <Field
                                    className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                    id="categoryName"
                                    placeholder={'Category name'}
                                    name="categoryName"
                                />
                            </div>
                            <div className='mt-4'>
                                <label className='block text-gray-700 text-sm font-bold mb-2' htmlFor="address">
                                    Address
                                </label>
                                <Field
                                    className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                    id="address"
                                    placeholder={'Address'}
                                    name="address"
                                />
                            </div>
                            <div className='mt-4'>
                                <label className='block text-gray-700 text-sm font-bold mb-2' htmlFor="latitude">
                                    Latitude
                                </label>
                                <Field
                                    className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                    id="latitude"
                                    placeholder={'Latitude'}
                                    name="latitude"
                                />
                            </div>
                            <div className='mt-4'>
                                <label className='block text-gray-700 text-sm font-bold mb-2' htmlFor="longitude">
                                    Longitude
                                </label>
                                <Field
                                    className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                    id="longitude"
                                    placeholder={'Longitude'}
                                    name="longitude"
                                />
                            </div>
                            <div className='mt-4'>
                                <label className='block text-gray-700 text-sm font-bold mb-2' htmlFor="longitude">
                                    Longitude
                                </label>
                                <input
                                    className="appearance-none w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                    id="longitude"
                                    placeholder={'Longitude'}
                                    name="longitude"
                                    type="file"
                                />
                            </div>
                            <div className="flex items-center justify-between mt-4">
                            <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline" type="submit">
                                Create
                            </button>
                            </div>
                            
                        </Form>
                    </div>
				)}
			</Formik>
      </>
  )
}

export default CreatePlace