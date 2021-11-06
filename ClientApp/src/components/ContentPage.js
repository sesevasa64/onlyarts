import React, {Component} from 'react'

import {LineProfileData} from './MiniCard'

import '../OnlyArts.css'
import './ContentPage.css'

class ContentPage extends Component
{
    constructor(props)
    {
        super(props);
    }

    renderImages()
    {
        const images_box = [];
        for(let i = 0; i < this.props.item.images.length; i++)
            images_box.push(
                <img src={this.props.item.images[i]} width="100%" height="400px"></img>
            )
        return images_box;
    }

    render() {
        const images = this.renderImages();
        return(
        <div className="content-page">
        <div className="left-box">
            {images}
        </div>
        <div className="right-box">
            <LineProfileData item={this.props.item}/>
        </div>
        </div>
    )
    }
}

export default ContentPage;
