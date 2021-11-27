import React, {Component, useState, useEffect} from 'react';
import {useParams, useRouteMatch} from 'react-router-dom';
import './css/UserPage.css';
import '../OnlyArts.css';

function UserPage(props)
{
    const [error, setError] = useState(null);
    const [isLoaded, setIsLoaded] = useState(false);
    const [user, setItems] = useState([]);
    const match = useRouteMatch({
        path: '/UserPage/:login',
        strict: true,
        sensitive: true,
      });
    useEffect(() => {
      fetch(`https://localhost:5001/api/users?login=${match.params.login}`)
        .then(res => res.json())
        .then(
          (result) => {
            setIsLoaded(true);
            
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
      if(user.length!= 0){
        console.log(user);
        return (
          <div className="main-content-block">
              <div className="user-info-box">
                  <div className="user-avatar-container">
                      <img className="user-avatar" width="200px" height="200px" src={user.LinkToAvatar}></img>
                  </div>
                  <div className="user-text">
                      <p className="user-nickname">{user.Nickname}</p>
                      <p className="user-about-header">Обо мне</p>
                      <p className="user-about">{user.Info || "Да-да, инфы нет, соре. ПацаНы!!"}</p>
                  </div>
              </div>
              {props.content}
          </div>
          );
      }
      else{
        return <div>Загрузка ...</div>
      }
    }
}

export default UserPage;
