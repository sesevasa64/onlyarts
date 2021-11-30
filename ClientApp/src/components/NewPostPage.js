import React, {Component} from 'react'

import './css/NewPostPage.css'
import RoundButton from './RoundButton';
import DragAndDrop from './DragAndDrop';
import LoadingPage from './LoadingPage';
import SuccessfulPage from './SuccessfulPage';

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
            
            contentIsSuccessfulLoad: false,
            contentIsPushing: false,
            flagStart: true,
        };
        this.pushContentCallback = this.pushContentCallback.bind(this);
        this.publishOnClick = this.publishOnClick.bind(this);
        this.selectLoadedImages = this.selectLoadedImages.bind(this);
        this.addFiles = this.addFiles.bind(this);
    }
    /*
    Функция, обрабатывающая событие onchange компонента select для того, чтобы отобразить выбранное пользователем изображение в поле. 
    */
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
    /*
    Функция, занимающаяся генерацией компонента select, а именно
    она загружает из массива items название имен файлов в компонент select и возвращаяет вновь созданный компонент
    */
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
    /*
        Функция ограничивающая ввод в строку имя
    */
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
    //Функция проверяющая корректность длины имени
    nameLengthIsCorrect = (name) => name.length >= 3 && name.length <= 30 

    //Функция проверяющая корректность длины описания
    descriptionLengthIsCorrect = (name) => name.length >= 3 && name.length <= 300

    /*
    Функция ограничивающая ввод в строку описания
    */
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
    /*
        Функция добавляющая новые файлы в массив файлов.
        new_file - указатель на файл, который будет добавлен в массив files
    */
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
    /*
    Функция преобразующая массив файлов в строку в формате base64,
    fails - массив файлов
    callback - функция, вызывающаяся после того, как все файлы были загружены, 
    принимающая параметры Images - массив изображений в формате строк base64
    */
    getImagesAsBase64(files, callback)
    {
        let Images = [];
        if(files.length === 0)
        {
            callback(Images);
        }
        for(let i = 0; i < files.length; i++)
        {
            let reader = new FileReader();
            reader.readAsDataURL(files[i]);
            reader.onload = () => {
                let image_base64 = reader.result;
                let from = image_base64.search(',') + 1;
                Images.push(image_base64.substring(from, image_base64.length));
                if(i == files.length - 1)
                {
                    callback(Images);
                }
            }
        }
        return Images;
    }
    //Функция обрабатывающая нажатие на кнопку "Опубликовать"
    publishOnClick()
    {
        if(this.nameLengthIsCorrect(this.state.Name) && this.descriptionLengthIsCorrect(this.state.Description))
        {
            this.setState({
                contentIsPushing: true,
            });
            console.log("Publish click");
            let callback = (Images) => {
                this.props.addNewPost({
                    Name: this.state.Name,
                    Description: this.state.Description,
                    ContentType: "aboba",
                    LinkToPreview: "aboba",
                    LinkToBlur: "aboba",
                    UserID: this.props.User.Id,
                    SubTypeID: 1,
                    Images: Images
                }, this.pushContentCallback);
            }
            this.getImagesAsBase64(this.state.files, callback);
        }
        else{
            this.setState({
                flagStart: false,
            })
        }
    }
    //Функция, вызываемая после попытки загрузки контенты
    pushContentCallback(result)
    {
        this.setState({
            contentIsSuccessfulLoad: result,
            contentIsPushing: false,
            flagStart: false,
        })
    }
    //Функция, занимающаяся генерацией лейбла, распологающегося над полем ввода имени. Внутри используются внутренние состояния компонента
    renderNameLabel()
    {
        /*Проверка на flagStart здесь нужна для того, чтобы в тот момент, когда пользователь открыл компонент первый раз
        У него не было подсвеченно красным, что он сделал что-то не так
        */
        return(
            this.nameLengthIsCorrect(this.state.Name) || this.state.flagStart ? 
            <label>Название (Осталось символов: {this.state.maxNameCount - this.state.curNameLength})</label> :
            <label className="error-message">Длина имени должна быть от 3 до 30 (Включительно)</label>
        )
    }
    //Функция, занимающаяся генерацией лейбла, распологающегося над полем ввода "Описание". Внутри используются внутренние состояния компонента
    renderDescriptionLabel()
    {
        /*Проверка на flagStart здесь нужна для того, чтобы в тот момент, когда пользователь открыл компонент первый раз
        У него не было подсвеченно красным, что он сделал что-то не так
        */
        return(
            this.descriptionLengthIsCorrect(this.state.Description) || this.state.flagStart ?
            <label> О посте (Осталось символов: {this.state.maxAboutCount - this.state.curAboutLength})</label> :
            <label className="error-message">Длина описания должна быть от 3 до 300 (Включительно)</label>
        )
    }

    render()
    {
        if(this.state.contentIsPushing)
        {
            return(
                <div className="main-content-block">
                    <div className="content-page">
                        <LoadingPage/>
                    </div>
                </div>
            )
        }
        else if(this.state.contentIsSuccessfulLoad)
        {
            return(
                <div className="main-content-block">
                    <div className="content-page">
                        <SuccessfulPage/>
                    </div>
                </div>
            )
        }
        console.log(this.state.flagStart)
        return(
                <div className="main-content-block">
                    <div className="content-page create-post-page">
                        <h2>Новый пост</h2>
                        {!this.state.contentIsLoad && !this.state.flagStart ? <p className="error-message">Проверьте корректность введеных данных</p> : ""}
                        {this.renderNameLabel()}
                        <input type="text" onChange={this.changeNameLength} defaultValue={this.state.Name}></input>
                        {this.renderDescriptionLabel()}
                        <textarea onChange={this.changeAboutLength} defaultValue={this.state.Description}></textarea>
                        <div className="add-images-block">
                            <div>
                                <img src={this.state.img}></img>
                            </div>
                            {this.renderSelectBox(this.state.files)}
                        </div>
                        <DragAndDrop addFiles={this.addFiles}/>
                        <RoundButton value="Опубликовать" onClick={this.publishOnClick}></RoundButton>
                        {this.state.contentIsLoad && <p>Контент успешно загружен</p>}
                    </div>
                </div>
            )
    }
}

export default NewPostPage;
