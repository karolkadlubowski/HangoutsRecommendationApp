import axios from 'axios';
import { useEffect, useState } from 'react';
import { VenueDetailsResponse } from '../../Services/Models';
import Loader from '../Loader/Loader';

interface VenueDetailsProps {
    VenueId?: number;
    closeModal: () => void;
}

export default function VenueDetails(props: VenueDetailsProps) {
    const [venueDetails, setVenueDetails] = useState<VenueDetailsResponse>();
    const [imgUrl, setImgUrl] = useState<any>();

    useEffect(() => {
        axios
            .get('http://localhost:8000/api/v1/Venue', {
                params: {
                    VenueId: props.VenueId,
                },
            })
            .then((response) => {
                console.log(response.data.venue.photos);
                setVenueDetails(response.data.venue);

                // const imageBlob = response.data.venue.photos[0];
                // const reader = new FileReader();
                // reader.readAsDataURL(imageBlob);
                // reader.onloadend = () => {
                //     const base64data = reader.result;
                //     setImgUrl(base64data);
                // };
                setImgUrl(response.data.venue.photos[0].fileUrl);
            });
    }, []);

    return (
        <>
            {props.VenueId ? (
                <>
                    <div className="justify-center items-center flex overflow-x-hidden overflow-y-auto fixed inset-0 z-50 outline-none focus:outline-none">
                        <div className="relative w-auto my-6 mx-auto max-w-5xl" style={{ minWidth: '50rem' }}>
                            {/*content*/}
                            <div className="border-0 rounded-lg shadow-lg relative flex flex-col w-full bg-white outline-none focus:outline-none">
                                {/*header*/}
                                <div className="flex items-start justify-between p-5 border-b border-solid border-slate-200 rounded-t">
                                    <h3 className="text-3xl font-semibold">{venueDetails?.name}</h3>
                                    <button
                                        className="p-1 ml-auto bg-transparent border-0 text-black float-right text-3xl leading-none font-semibold outline-none focus:outline-none"
                                        onClick={() => props.closeModal()}
                                    >
                                        <span className="bg-transparent text-black h-6 w-6 text-2xl block outline-none focus:outline-none">
                                            ×
                                        </span>
                                    </button>
                                </div>

                                <div className="relative p-6 flex-auto">
                                    {venueDetails ? (
                                        <>
                                            <div className="my-4 text-slate-500 text-lg leading-relaxed">
                                                <img src={imgUrl} alt="" className="mb-5" />
                                                {venueDetails?.description}
                                            </div>
                                            <p className="my-4 text-slate-1200 text-lg leading-relaxed">
                                                Location: {venueDetails.location.address}
                                            </p>
                                        </>
                                    ) : (
                                        <Loader />
                                    )}
                                </div>

                                <div className="flex items-center justify-end p-6 border-t border-solid border-slate-200 rounded-b">
                                    <button
                                        className="text-red-500 background-transparent font-bold uppercase px-6 py-2 text-sm outline-none focus:outline-none mr-1 mb-1 ease-linear transition-all duration-150"
                                        type="button"
                                        onClick={() => props.closeModal()}
                                    >
                                        Close
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="opacity-25 fixed inset-0 z-40 bg-black"></div>
                </>
            ) : null}
        </>
    );
}
