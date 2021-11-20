import React, {Component, useState, useEffect} from 'react'
import {useParams, useRouteMatch} from 'react-router-dom';

import {LineProfileData} from './LineProfileData'

import '../OnlyArts.css'
import './css/ContentPage.css'

function timeout(delay) {
    return new Promise( res => setTimeout(res, delay) );
}

function renderImages(content_item)
{
    const images_box = [];
    for(let i = 0; i < 3; i++)
        images_box.push(
            <img src={content_item.linkToPreview} width="100%" height="400px"></img>
        )
    return images_box;
}


function ContentPage(props) {
    const [error, setError] = useState(null);
    const [isLoaded, setIsLoaded] = useState(false);
    const [items, setItems] = useState([]);
    const match = useRouteMatch({
        path: '/ContentPage/:contentId',
        strict: true,
        sensitive: true,
      });

    useEffect(() => {
      fetch(`https://localhost:5001/api/contents/${match.params.contentId}`)
        .then(res => res.json())
        .then(
          (result) => {
            setIsLoaded(true);
            console.log(result);
            setItems(result);
          },
          (error) => {
            setIsLoaded(true);
            setError(error);
          }
        )
    }, [])
    if (error) {
      return <div>Ошибка: {error.message}</div>;
    } else if (!isLoaded) {
      return <div className="content-page">Загрузка...</div>;
    } else
    {
        console.log(items)
        if(items.length != 0){
            return (
                <div className="main-content-block">
                    <div className="content-page">
                        <div className="left-flex-box">
                            {renderImages(items)}
                        </div>
                        <div className="right-flex-box">
                            <LineProfileData onLikeClick={() => props.onLikeClick(items.id)} item={items}/>
                        </div>
                    </div>
                </div>
            );
        }
        else{
            return <div>Загрузка...</div>
        }
    }
}
/*items.map(current_item =>(
        <div className="main-content-block">
            <div className="content-page">
                <div className="left-flex-box">
                    {renderImages(current_item)}
                </div>
                <div className="right-flex-box">
                    <LineProfileData item={current_item}/>
                </div>
            </div>
        </div>))*/
/*
function ContentPage (props)
{
    const {contentId} = useParams();
    let primise = getContentById(contentId);
    primise.then((content_item)=>
    {
        let images;
        alert(content_item);

        images = renderImages(content_item);
        return(
            <div className="content-page">
            <div className="left-flex-box">
                {images}
            </div>
            <div className="right-flex-box">
                <LineProfileData content_item={content_item}/>
            </div>
            </div>
        )
    })
} 

class ContentPage extends Component
{
    constructor(props)
    {
        super(props);
        this.setState({
            //contentId: props.match.params.contentId,
            content_item: Object,
            isLoaded: false,
        });
    }

    renderImages(content_item)
    {
        const images_box = [];
        for(let i = 0; i < 3; i++)
            images_box.push(
                <img src={content_item.linkToPreview} width="100%" height="400px"></img>
            )
        return images_box;
    }
    componentDidMount()
    {
        fetch(`https://localhost:5001/api/contents/${1}`)
        .then((response)=>response.json())
        .then((response)=>
        {
            this.setState({content_item: response});
            this.setState({isLoaded: true});
        })
    }

    render() {
        const {isLoaded, content_item } = this.state;
        if(isLoaded)
        {
            const images = this.renderImages(content_item);
            return(
            <div className="content-page">
            <div className="left-flex-box">
                {images}
            </div>
            <div className="right-flex-box">
                <LineProfileData content_item ={content_item}/>
            </div>
            </div>
          )
        }
        else
            return <div></div>
    }
}*/

export default ContentPage;
