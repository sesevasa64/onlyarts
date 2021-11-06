import React from 'react'
import '../OnlyArts.css'
import './MiniCard.css'

function MiniCard(props)
{
    return(
        <div className="mini-card-box">
          <div className="left-box">
            <img src={props.item.facial_image}/>
          </div>
          <div className="right-box">
            <LineProfileData item={props.item}/>
          </div>
        </div>
    )
}
//https://i.ytimg.com/vi/4lifQfeZo5c/maxresdefault.jpg
//<LineProfileData/>

function LineProfileData(props)
{
  return (
    <div className="profile-line">
      <div>
      <p>{props.item.caption}</p>
      <img src={props.item.user_image}/>
      <p>{props.item.user_name}</p>
      <div className="mini-card-about">
        {props.item.short_about}
      </div>
      </div>
    </div>
  )
}

export {MiniCard, LineProfileData};