import React from 'react'
import {Link} from 'react-router-dom'
import './css/MiniCard.css'
import '../OnlyArts.css'
import { LineProfileData } from './LineProfileData'
import views from './resources/views.png';
import likes from './resources/likes.png';

function MiniCard(props)
{
    var user_obj = props.item.User;
    if(!user_obj)
    {
      return(
        <div key={props.mc_key}></div>
      )
    }

    return(
        <div key={props.mc_key} className="mini-card-box" onClick={() => props.onClick(props.item)}>
          <Link key={`$mc-link-${props.mc_key}`} to={`/ContentPage/${props.item.Id}`}>
            <img key={`$mc-img-blur-${props.mc_key}`} src={props.item.LinkToBlur.length > 15 ? props.item.LinkToBlur : "https://i.ytimg.com/vi/1C0TWnIbsKQ/maxresdefault.jpg"}/>
            <div key={`$mc-hidden-info-${props.mc_key}`} className="hidden-info">
              <p key={`$mc-p-${props.mc_key}`}>
              <img key={`$mc-img-profile-${props.mc_key}`} src={user_obj.LinkToAvatar}/>{user_obj.Nickname}<br/>
                {props.item.Name}
                </p>
            </div>
            </Link>
        </div>
    )
}

export {MiniCard};