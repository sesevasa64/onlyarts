import React from 'react'
import {Link} from 'react-router-dom'
import '../OnlyArts.css'
import './css/LineProfileData.css'
import views from './resources/views.png';
import likes from './resources/likes.png';
import dilike from './resources/dislike.png';

function LineProfileData(props)
{
  let User = props.item[0].User;
  console.log(User);
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
        <div className='line-stats'>
          <img src={views}></img> {props.item[0].ViewCount}
          <img  className={`likes_button ${!props.liked || 'liked'}`} src={likes} onClick={props.onLikeClick}/> {props.item[0].LikesCount}
          <img  className={`likes_button`} src={dilike}/>{props.item[0].DislikesCount}
        </div>
    </div>
  )
}

export {LineProfileData};
