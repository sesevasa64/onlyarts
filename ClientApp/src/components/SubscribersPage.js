import React, {Component, useState, useEffect} from 'react'
import {Link, NavLink, useParams, useRouteMatch} from 'react-router-dom';

import './css/UserCard.css'

let host_name = "https://" + document.location.host;



class SubscribersPage extends Component
{
    constructor(props)
    {
        super(props);
        this.state = {
            contentIsSuccessfulLoad: true,
            contentIsLoading: false,
        };
    }

    renderUsersCards(users)
    {
        let items = [];
        for(let i = 0; i < users.length; i++)
        {
            items.push(
                <div className='user-card'>
                    <a onClick={() => {
                        console.log("Privet");
                        this.props.isNext(true)}} href={`/UserPage/${users[i].Login}`}>
                        <img src={`${users[i].LinkToAvatar}`}></img>
                        <p>{users[i].Nickname}</p>
                    </a>
                </div>
            );
        }
        return items;
    }

    render()
    {
        let items = this.renderUsersCards(this.props.users);
        return(
            <div>
                <h3>Подписчики</h3>
                <div className='user-cards'>
                    {items}
                </div>
            </div>
        );
    }
    
}

export {SubscribersPage};
