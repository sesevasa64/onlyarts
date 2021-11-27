import React from 'react'
import {Link} from 'react-router-dom'
import '../OnlyArts.css'
import './css/LineProfileData.css'
import views from './resources/views.png';
import likes from './resources/likes.png';

function LineProfileData(props)
{
  return (
    <div className="profile-line">
        <p className="content-name">{props.item.Name}</p><br/> 
        <div>
          <img src={props.item.User.LinkToAvatar}/>
          <p><Link to={`/UserPage/${props.item.User.Login}`}>{props.item.User.Nickname}</Link></p>
        </div>
        <div className="mini-card-about">
          {props.item.Description}
        </div>
        <div>
          <img src={views}></img> {props.item.ViewCount}
          <img className="likes_button" src={likes} onClick={props.onLikeClick}></img> {props.item.LikesCount}
        </div>
      
    </div>
  )
}

export {LineProfileData};
