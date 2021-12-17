import React, {Component, useState, useEffect} from 'react'
import {useParams, useRouteMatch} from 'react-router-dom';

import {LineProfileData} from './LineProfileData'
import LoadingPage from './LoadingPage';

import '../OnlyArts.css'
import './css/ContentPage.css'

let host_name = "https://" + document.location.host;

function timeout(delay) {
    return new Promise( res => setTimeout(res, delay) );
}

function getImagesByContentId(id, callback_func)
{
    fetch(`${host_name}/api/images/contents/${id}`)
    .then((response)=>{
        if(response.ok)
        {
            return response.json();
        }
        else
        {
            return 0;
        }
    })
    .then((value)=>{
        if(value){
            console.log(value);
            callback_func(value);
        }
        callback_func(0);
    })
}

function renderImages(images)
{
    const images_box = [];
    for(let i = 0; i < images.length; i++)
        images_box.push(
            <div className="container-img">
                <img src={images[i]}></img>
            </div>
        );
    return images_box;
}


function ContentPage(props) {
    const [error, setError] = useState(null);
    const [isLoaded, setIsLoaded] = useState(false);
    const [items, setItems] = useState([]);
    const [images, setImages] = useState([]);
    const match = useRouteMatch({
        path: '/ContentPage/:contentId',
        strict: true,
        sensitive: true,
      });
    useEffect(() => {
      fetch(`${host_name}/api/contents/${match.params.contentId}`)
        .then(res => res.json())
        .then(
          (result) => {
            setIsLoaded(true);
            setItems(result);
            props.addViewToContent(match.params.contentId);
          },
          (error) => {
            setIsLoaded(true);
            setError(error);
          }
        )
        .then((value)=>{
            getImagesByContentId(match.params.contentId, (result)=>
            {
                if(result)
                {
                    setIsLoaded(true);
                    setImages(result);
                }
            })
        })
    }, [])
    if (error) {
      return <div>Ошибка: {error.message}</div>;
    } else if (!isLoaded) {
      return (
        <div className="main-content-block">
            <div className="content-page">
                <LoadingPage/>
            </div>
        </div>
        );
    } else
    {
        if(items.length != 0){
            return (
                <div className="main-content-block">
                    <div className="content-page">
                        <div className="left-flex-box">
                            <div className="content-container">
                                {renderImages(images)}
                            </div>
                        </div>
                        <div className="right-flex-box">
                            <LineProfileData onLikeClick={() => props.onLikeClick(items.id)} item={items}/>
                        </div>
                    </div>
                </div>
            );
        }
        else{
            return (
            <div className="main-content-block">
                <div className="content-page">
                    <LoadingPage/>
                </div>
            </div>
            )
        }
    }
}

export default ContentPage;
