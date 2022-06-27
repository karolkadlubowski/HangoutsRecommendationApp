import axios from 'axios';
import { useEffect, useState } from 'react';
import { FavouritesResponse } from '../../Services/Models';
import VenueDetails from '../FindVenue/VenueDetails';
import Header from '../Header';

export default function Favourites() {
    const token = localStorage.getItem('token');
    const [venueList, setVenueList] = useState<FavouritesResponse[]>();
    const [showModal, setShowModal] = useState(false);
    const [modalId, setModalId] = useState<number>();

    useEffect(() => {
        axios
            .get('http://localhost:8003/api/v1/List/Favorites', {
                params: {
                    pageNumber: 1,
                    pageSize: 100,
                },
                headers: { 'Content-Type': 'application/json', 'Authorization': `Bearer ${token}` },
            })
            .then((response) => {
                console.log(response);
                setVenueList(response.data.favorites);
            })
            .catch(function (response) {
                console.log(response);
            });
    }, []);

    const writeVenues = () => {
        return venueList?.map((el) => (
            <button
                key={el.venueId}
                type="button"
                onClick={() => openModal(el.venueId)}
                className="
        text-left
        px-6
        py-2
        border-b border-gray-200
        w-full
        hover:bg-gray-100 hover:text-gray-500
        focus:outline-none focus:ring-0 focus:bg-gray-200 focus:text-gray-600
        transition
        duration-500
        cursor-pointer
      "
            >
                Name: {el.name}, VenueId: {el.venueId}
            </button>
        ));
    };

    const openModal = (id: number) => {
        setShowModal(true);
        setModalId(id);
    };

    return (
        <>
            <Header />
            <div className="flex justify-center mt-5">
                <div className="bg-white rounded-lg border border-gray-200 w-96 text-gray-900">{writeVenues()}</div>
            </div>

            {showModal ? (
                <VenueDetails
                    VenueId={modalId}
                    closeModal={() => {
                        setShowModal(false);
                    }}
                />
            ) : null}
        </>
    );
}
