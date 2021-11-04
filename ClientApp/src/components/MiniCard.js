import React from 'react'
import '../OnlyArts.css'
import './MiniCard.css'

function MiniCard(props)
{
    return(
        <div className="mini-card-box">
          <div className="left-box">
            <img src={props.src}/>
          </div>
          <div className="right-box">
            <LineProfileData/>
          </div>
        </div>
    )
}
//https://i.ytimg.com/vi/4lifQfeZo5c/maxresdefault.jpg
//<LineProfileData/>

function LineProfileData()
{
  return (
    <div className="profile-line">
      <div>
      <p>My Aboba</p>
      <img src="https://gottadotherightthing.com/wp-content/uploads/2018/12/beautiful-blur-blurred-background-733872-1.jpg"/>
      <p>Diana3821</p>
      <div className="mini-card-about">
      Well organized and easy to understand Web building tutorials with lots of examples of how to use HTML, CSS, JavaScript, SQL, Python, PHP,
      Well organized and easy to understand Web building tutorials with lots of examples of how to use HTML, CSS, JavaScript, SQL, Python, PHP,
      Well organized and easy to understand Web building tutorials with lots of examples of how to use HTML, CSS, JavaScript, SQL, Python, PHP,
      Well organized and easy to understand Web building tutorials with lots of examples of how to use HTML, CSS, JavaScript, SQL, Python, PHP,
      Well organized and easy to understand Web building tutorials with lots of examples of how to use HTML, CSS, JavaScript, SQL, Python, PHP,
      </div>
      </div>
    </div>
  )
}

export default MiniCard;