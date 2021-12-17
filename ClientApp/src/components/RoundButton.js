import React from 'react'
import './css/RoundButton.css'

function RoundButton(props)
{
    return(
        <button className={`round-button ${props.className}`} onClick={() => props.onClick(true)}>
            {props.value}
        </button>
    )
}

export default RoundButton;
