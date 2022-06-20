import { library } from '@fortawesome/fontawesome-svg-core';
import { faBowlFood, faFontAwesome, fas } from '@fortawesome/free-solid-svg-icons';
import axios from 'axios';
import { useEffect, useState } from 'react';
import toast from 'react-hot-toast';
import TinderCard from 'react-tinder-card';
import Header from '../Header';
import './FindPlace.css';

interface VenueDetails {
    id?: number;
    name?: string;
    img?: any;
}

export default function FindPlace() {
    const [direction, setDirection] = useState<string>();
    const [id, setId] = useState<number>();
    const [VenueDetails, setVenueDetails] = useState<VenueDetails[]>();
    const [venueIds, setVenueIds] = useState<number[]>();
    library.add(fas, faBowlFood, faFontAwesome);

    const getVenueIds = () => {
        const token = localStorage.getItem('token');
        axios({
            method: 'get',
            url: 'http://localhost:5000/venue/algorithm/venues',
            headers: { 'Content-Type': 'application/json', 'Authorization': `Bearer ${token}` },
        })
            .then(function (response) {
                setVenueIds(response?.data?.data?.venueIds);
            })
            .catch(function (response) {
                console.log(response);
                toast.error('Error');
            });
    };

    const getVenueDetails = (ids: number[]) => {
        const requestOne = axios.get('http://localhost:8000/api/v1/Venue', {
            params: {
                VenueId: ids[0],
            },
        });
        const requestTwo = axios.get('http://localhost:8000/api/v1/Venue', {
            params: {
                VenueId: ids[1],
            },
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
                    setVenueDetails([secondVenueDetails, firstVenueDetails]);
                })
            )
            .catch((errors) => {
                console.error(errors);
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

    const onSwipe = (direction: string) => {
        setDirection('You swiped: ' + direction);
    };

    const props = {
        onSwipe: onSwipe,
        preventSwipe: ['up', 'down'],
    };

    const OnCardLeftScreen = (id?: number) => {
        if (id) {
            setId(id);
            const newIds = venueIds;
            newIds?.shift();
            if (newIds) getVenueDetails(newIds);
        }
    };

    return (
        <div className="overflow-hidden">
            <Header />
            <div className="flex justify-center">
                <div className="flex flex-col content-center">
                    <div className="mt-10">
                        <p>{direction ?? 'Swiping direction'}</p>
                        <p>{`id: ${id} left the screen` ?? 'Swiping id'}</p>
                    </div>
                    <div className="cardContainer">
                        {VenueDetails?.map((el: VenueDetails) => (
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
                        ))}
                    </div>
                </div>
            </div>
        </div>
    );
}
