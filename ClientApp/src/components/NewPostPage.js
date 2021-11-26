import React, {Component} from 'react'

import './css/NewPostPage.css'
import RoundButton from './RoundButton';
import DragAndDrop from './DragAndDrop';

class NewPostPage extends Component
{
    constructor(props)
    {
        super(props);
        this.state = {
            Name: "",
            Description: "",
            ContentType: "",
            Link: "",

            files: [],
            selected_file: null,
            img: "https://vseprivoroti.ru/wp-content/uploads/2019/05/64554888.jpg",

            curNameLength: 0,
            maxNameCount: 30,
            curAboutLength: 0,
            maxAboutCount: 300,
        };
        this.selectLoadedImages = this.selectLoadedImages.bind(this);
        this.addFiles = this.addFiles.bind(this);
    }

    selectLoadedImages = (event) =>
    {
        let selected_file = this.state.files[event.target.selectedIndex];
        var reader = new FileReader();
        reader.readAsDataURL(selected_file);
        reader.onload = () => {
            this.setState({
                img: reader.result,
            })
        };
    }

    renderSelectBox(items)
    {        
        let elem_select = [];
        for(let i = 0; i < items.length; i++)
        {
            elem_select.push(
                <option value={items[i].name}>{items[i].name}</option>
            )
        }
        return(
            <select multiple id="loaded_images" onChange={this.selectLoadedImages}>
                {elem_select}
            </select>
        )
    }

    changeNameLength  = (event) =>
    {
        let Name = event.target.value;
        let curNameLength = Name.length;
        if(this.state.maxNameCount - curNameLength >= 0){
            this.setState({
                curNameLength: curNameLength,
                Name: Name,
            })
        }
        else
        {
            event.target.value = this.state.Name;
        }
    }

    changeAboutLength  = (event) =>
    {
        let Description = event.target.value;
        let curAboutLength = Description.length;
        if(this.state.maxAboutCount - curAboutLength >= 0){
            this.setState({
                curAboutLength: Description.length,
                Description: Description
            })

        }
        else{
            event.target.value = this.state.Description;
        }
    }

    addFiles(new_file)
    {
        if(new_file)
        {
            let new_files_list = [].concat(new_file).concat(this.state.files);
            this.setState({
                files: new_files_list
            })
        }
    }

    render()
    {
        console.log("render post page");
        return(
            <div className="main-content-block">
                <div className="content-page create-post-page">
                    <h2>Новый пост</h2>
                    <label>Название (Осталось символов: {this.state.maxNameCount - this.state.curNameLength})</label>
                    <input type="text" onChange={this.changeNameLength}></input>
                    <label>О посте (Осталось символов: {this.state.maxAboutCount - this.state.curAboutLength})</label>
                    <textarea onChange={this.changeAboutLength}></textarea>
                    <div className="add-images-block">
                        <div>
                            <img src={this.state.img}></img>
                        </div>
                        {this.renderSelectBox(this.state.files)}
                    </div>
                    <DragAndDrop addFiles={this.addFiles}/>
                    <RoundButton value="Опубликовать"></RoundButton>
                </div>
            </div>
        );
    }
}

export default NewPostPage;
