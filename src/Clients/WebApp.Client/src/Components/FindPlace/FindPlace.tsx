import { library } from '@fortawesome/fontawesome-svg-core';
import { faBowlFood, faFontAwesome, fas } from '@fortawesome/free-solid-svg-icons';
import axios from 'axios';
import { useEffect, useState } from 'react';
import TinderCard from 'react-tinder-card';
import Header from '../Header';
import './FindPlace.css';

export const favi: any[] = [];
export default function FindPlace() {
    const [direction, setDirection] = useState<string>()
    const [favorite, setFavorite] = useState<boolean>()
    const [id, setId] = useState<number>()
    const [people, setPeople] = useState([]);
    library.add(fas, faBowlFood,  faFontAwesome)

    useEffect(() => {
        axios
          .get(
            "https://api.themoviedb.org/3/trending/all/day?api_key=360a9b5e0dea438bac3f653b0e73af47&language=en-US"
          )
          .then((res) => setPeople(res.data.results.reverse()));
    }, []);

    const onSwipe = (direction: string) => {
        setDirection('You swiped: ' + direction)
    }

    const props = {
        onSwipe: onSwipe,
        preventSwipe: ['up', 'down']
    }

    const addToFavorite = (id: number) => {
        let currentFavorites: string[] = JSON.parse(localStorage.getItem("favorites") ?? '');
        currentFavorites.push(id.toString());
        setFavorite(true);
        localStorage.setItem("favorites", JSON.stringify(currentFavorites));
        console.log(currentFavorites);
    }

    const removeFromFavorites = (id: number) => {
      let currentFavorites: string[] = JSON.parse(localStorage.getItem("favorites") ?? '');
      const index = currentFavorites.indexOf(id.toString());
      if (index > -1) {
        currentFavorites.splice(index, 1);
      }
      setFavorite(false);
      localStorage.setItem("favorites", JSON.stringify(currentFavorites));
      console.log(currentFavorites);
    }

  return (
    <div className='overflow-hidden'>
        <Header/>
        <div className='flex justify-center'>
            <div className='flex flex-col content-center'>
              <div className='mt-10'>
                <p>{direction ?? 'Swiping direction'}</p>
                <p>{`id: ${id} left the screen` ?? 'Swiping id'}</p>
              </div>
              {/* {!favorite ? 
                <button onClick={() => {if(id){addToFavorite(id)}}}>
                  <FontAwesomeIcon icon={faHeartCirclePlus} className="ml-5 text-2xl leading-lg text-black opacity-75"/>
                </button> : 
                <button onClick={() => {if(id){removeFromFavorites(id)}}}>
                  <FontAwesomeIcon icon={faHeartCircleMinus} className="ml-5 text-2xl leading-lg text-red-600 opacity-75"/>
                </button>
                } */}
            <div className='mt-10 flex justify-center'>
                {people.map((k: any, index: number) => (
                <TinderCard {...(props as any)} 
                            className="swipe"
                            onCardLeftScreen={() => setId(index)} 
                            onSwipe={onSwipe}
                            preventSwipe={['up', 'down']}>
            <div
              style={{
                backgroundImage: `url(https://image.tmdb.org/t/p/w500${k.poster_path})`
              }}
              className="card"
            >
              <h4
                className='text-background'
              >
                {k?.original_title}
              </h4>
            </div>
                </TinderCard>
                ))}
                
            </div>
            </div>
      </div>
    </div>
  )
}