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
      <div>
        <p>{props.item.name}</p><br/>
        <img src={props.item.user.linkToAvatar}/>
        <p><Link to={`/UserPage/${props.item.user.login}`}>{props.item.user.nickname}</Link></p>
        <div className="mini-card-about">
          {props.item.description}
        </div>
        <div>
          <img src={views}></img> {props.item.viewCount}
          <img className="likes_button" src={likes} onClick={props.onLikeClick}></img> {props.item.likesCount}
        </div>
      </div>
    </div>
  )
}

export {LineProfileData};
