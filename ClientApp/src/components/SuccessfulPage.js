import React from "react";
import './css/SuccessfulPage.css'
import galka from './resources/galka.png'

function SuccessfulPage()
{
    return(
        <div className="successful-page">
            <div>
                <img src={galka}/>
                <p>Успешная загрузка</p>
            </div>
        </div>
    )
} 

export default SuccessfulPage;
