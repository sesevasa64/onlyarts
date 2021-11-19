import React, {Component, useState, useEffect} from 'react';
import {useParams, useRouteMatch} from 'react-router-dom';
import './css/UserPage.css';
import '../OnlyArts.css';

function UserPage(props)
{
    const [error, setError] = useState(null);
    const [isLoaded, setIsLoaded] = useState(false);
    const [items, setItems] = useState([]);
    const match = useRouteMatch({
        path: '/UserPage/:login',
        strict: true,
        sensitive: true,
      });

    useEffect(() => {
      fetch(`https://localhost:5001/api/users/${match.params.login}`)
        .then(res => res.json())
        .then(
          (result) => {
            setIsLoaded(true);
            console.log(result);
            setItems(result);
          },
          (error) => {
            setIsLoaded(true);
            setError(error);
          }
        )
    }, [])
  
    if (error) {
      return <div>Ошибка: {error.message}</div>;
    } else if (!isLoaded) {
      return <div className="content-page">Загрузка...</div>;
    } else {
      return (
        items.map(user =>(
        <div className="main-content-block">
            <div className="user-info-box">
                <div className="user-avatar-container">
                    <img className="user-avatar" width="200px" height="200px" src={user.linkToAvatar}></img>
                </div>
                <div className="user-text">
                    <p className="user-nickname">{user.nickname}</p>
                    <p className="user-about-header">Обо мне</p>
                    <p className="user-about">Ну да я Леха, хочешь узнать, почему я Леха. Ну просто Лёха, ну так, Лёха. Назвали меня так</p>
                </div>
            </div>
            {props.content}
        </div>
        ))
      );
    }
}

export default UserPage;
