import { library } from '@fortawesome/fontawesome-svg-core';
import { faFontAwesome, faHeart, fas } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import axios from 'axios';
import { useEffect, useState } from 'react';
import toast from 'react-hot-toast';
import TinderCard from 'react-tinder-card';
import VenueDetails from '../FindVenue/VenueDetails';
import Header from '../Header';
import './FindPlace.css';

interface VenueDetails {
    id?: number;
    name?: string;
    img?: any;
}

export default function FindPlace() {
    const token = localStorage.getItem('token');
    const [direction, setDirection] = useState<string>();
    const [id, setId] = useState<number>();
    const [VenueDetailsInfo, setVenueDetailsInfo] = useState<VenueDetails[]>();
    const [venueIds, setVenueIds] = useState<number[]>();
    const [showModal, setShowModal] = useState(false);
    const [modalId, setModalId] = useState<number>();
    library.add(fas, faHeart, faFontAwesome);

    const getVenueIds = () => {
        const token = localStorage.getItem('token');
        axios({
            method: 'get',
            url: 'http://localhost:5000/venue/algorithm/venues',
            headers: { 'Content-Type': 'application/json', 'Authorization': `Bearer ${token}` },
        })
            .then(function (response) {
                console.log(response?.data?.data?.venueIds);
                setVenueIds(response?.data?.data?.venueIds);
            })
            .catch(function (response) {
                console.log(response);
                toast.error('Error');
            });
    };

    const addToFavorites = () => {
        const token = localStorage.getItem('token');
        const id = VenueDetailsInfo ? VenueDetailsInfo[0].id : null;
        axios({
            method: 'post',
            url: 'http://localhost:8003/api/v1/List/Favorites',
            data: {
                venueId: id,
                name: 'name',
                description: 'des',
                categoryName: 'NATURE',
                creatorId: 1,
            },
            headers: { 'Content-Type': 'application/json', 'Authorization': `Bearer ${token}` },
        })
            .then(function (response) {
                console.log(response?.data?.data?.venueIds);
                toast.success('Added to favorites');
            })
            .catch(function (response) {
                console.log(response);
                toast.error('Failed to add to favorites');
            });
    };

    const getVenueDetails = (ids: number[]) => {
        const requestOne = axios.get('http://localhost:8000/api/v1/Venue', {
            params: {
                VenueId: ids[0],
            },
            headers: { Authorization: `Bearer ${token}` },
        });
        const requestTwo = axios.get('http://localhost:8000/api/v1/Venue', {
            params: {
                VenueId: ids[1],
            },
            headers: { Authorization: `Bearer ${token}` },
        });
        axios
            .all([requestOne, requestTwo])
            .then(
                axios.spread((...responses) => {
                    const responseOne = responses[0];
                    const firstVenueDetails = {
                        id: responseOne.data.venue.venueId,
                        name: responseOne.data.venue.name,
                        img: responseOne.data.venue.photos,
                    };
                    const responseTwo = responses[1];
                    const secondVenueDetails = {
                        id: responseTwo.data.venue.venueId,
                        name: responseTwo.data.venue.name,
                        img: responseTwo.data.venue.photos,
                    };
                    setVenueDetailsInfo([secondVenueDetails, firstVenueDetails]);
                })
            )
            .catch((errors) => {
                console.error(errors);
            });
    };

    const sendVenueInfo = (id: number) => {
        const token = localStorage.getItem('token');
        axios({
            method: 'put',
            url: 'http://localhost:5000/venue/algorithm/like',
            params: {
                venueId: id,
            },
            headers: { 'Content-Type': 'application/json', 'Authorization': `Bearer ${token}` },
        })
            .then(function (response) {
                console.log(response);
            })
            .catch(function (response) {
                console.log(response);
            });
    };

    useEffect(() => {
        getVenueIds();
    }, []);

    useEffect(() => {
        if (venueIds) {
            getVenueDetails(venueIds);
        }
    }, [venueIds]);

    useEffect(() => {
        if (id && direction) {
            console.log(id, direction);
            if (direction === 'right') {
                sendVenueInfo(id);
            }
        }
    }, [id]);

    const onSwipe = (direction: string) => {
        console.log('swipe');
        setDirection(direction);
    };

    const props = {
        onSwipe: onSwipe,
        preventSwipe: ['up', 'down'],
    };

    const OnCardLeftScreen = (id?: number) => {
        console.log('left');
        if (id) {
            setId(id);
            const newIds = venueIds;
            newIds?.shift();
            if (newIds) getVenueDetails(newIds);
        }
    };

    const onAddToFavorites = () => {
        console.log(VenueDetailsInfo && VenueDetailsInfo[0].id);
        if (id) {
            addToFavorites();
        }
    };

    const openModal = (id: number | null) => {
        if (id) {
            setShowModal(true);
            setModalId(id);
        }
    };

    return (
        <>
            <Header />
            <div className="flex justify-center bg-gray-100 page-height">
                <div className="bg-slate-100 w-full absolute flex justify-center">
                    <div className="flex flex-col items-center justify-center" style={{ height: '60vh' }}>
                        <div className="w-full text-right mb-10 mr-10">
                            <button onClick={() => onAddToFavorites()}>
                                <FontAwesomeIcon
                                    icon={faHeart}
                                    className="text-2xl leading-lg text-red-300 opacity-75"
                                />
                            </button>
                        </div>
                        <div className="cardContainer relative z-10">
                            {VenueDetailsInfo?.map((el: VenueDetails) => (
                                <button onClick={() => openModal(el.id ? el.id : null)}>
                                    <TinderCard
                                        {...(props as any)}
                                        className="swipe"
                                        onCardLeftScreen={() => OnCardLeftScreen(el.id)}
                                        onSwipe={onSwipe}
                                        key={el.id}
                                        preventSwipe={['up', 'down']}
                                    >
                                        <div
                                            style={{
                                                backgroundImage: `url(${el.img[0].fileUrl})`,
                                            }}
                                            className="card"
                                        >
                                            <h4 className="text-background">
                                                {el.name} {el.id}
                                            </h4>
                                        </div>
                                    </TinderCard>
                                </button>
                            ))}
                        </div>

                        {showModal ? (
                            <VenueDetails
                                VenueId={modalId}
                                closeModal={() => {
                                    setShowModal(false);
                                }}
                            />
                        ) : null}
                    </div>
                </div>
            </div>
        </>
    );
}
