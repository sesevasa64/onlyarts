import React from 'react'
import {Link} from 'react-router-dom'
import '../OnlyArts.css'
import './css/LineProfileData.css'
import views from './resources/views.png';
import likes from './resources/likes.png';

function LineProfileData(props)
{
  let User = props.item[0].User;
  return (
    <div className="profile-line">
        <p className="content-name">{props.item[0].Name}</p><br/> 
        <div>
          <img src={User.LinkToAvatar}/>
          <p><Link to={`/UserPage/${User.Login}`}>{User.Nickname}</Link></p>
        </div>
        <div className="mini-card-about">
          {props.item[0].Description}
        </div>
        <div>
          <img src={views}></img> {props.item[0].ViewCount}
          <img className="likes_button" src={likes} onClick={props.onLikeClick}></img> {props.item[0].LikesCount}
        </div>
      
    </div>
  )
}

export {LineProfileData};
