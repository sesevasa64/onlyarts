import React, {Component} from "react";
import './css/EditUserPage.css'
import LoadingPage from "./LoadingPage";
import RoundButton from "./RoundButton";
import SuccessfulPage from "./SuccessfulPage";

class EditUserPage extends Component
{
    constructor(props)
    {
        super(props)
        this.state = {
            Nickname: "",
            Info: "",
            
            curNicknameLength: 0,
            maxNicknameCount: 30,
            minNicknameLength: 3,
            
            curInfoLength: 0,
            minInfoLength: 3,
            maxInfoCount: 300,

            pageIsStart: true,
            userIsSuccessfulLoad: false,
            userIsPushing: false,
            NicknameCorrect: false,
            InfoCorrect: false,
        }
        this.onPushClick = this.onPushClick.bind(this);
    }

    onNicknameChange = (event) => {
        this.setState({
            Nickname: event.target.value,
            curNicknameLength: event.target.value.length, 
        });
    }
    onInfoChange = (event) => {this.setState({
        Info: event.target.value, 
        curInfoLength: event.target.value.length,
    });
    }

    checkNickname = (Nickname) => Nickname.length >= this.state.minNicknameLength 
    && this.state.maxNicknameCount >= Nickname.length

    checkInfo = (Info) => Info.length >= this.state.minInfoLength && this.state.maxInfoCount >= Info.length

    onPushClick = (event) =>
    {
        this.setState({
            userIsPushing: true,
            pageIsStart: false,
        })
        var NewUser = {
            Id: this.props.User.Id,
            Nickname: this.state.Nickname,
            Info: this.state.Info
        }
        this.props.changeUserInfo(NewUser, (result)=>
        {
            this.setState({
                userIsSuccessfulLoad: result,
                userIsPushing: false
            })
        })
    }

    renderNicknameLabel()
    {
        return(
            this.checkNickname(this.state.Nickname) && !this.state.pageIsStart ?
             <label>Изменить Nickname</label> :
             <label className="error-message">Никнейм должен иметь длину от {this.state.minNicknameLength} до {this.state.maxNicknameCount}</label>
        )
    }

    renderInfoLabel()
    {
        return(
            this.checkInfoname(this.state.Info) && !this.state.pageIsStart ?
             <label>Изменить информацию о себе</label> :
             <label className="error-message">Описание должно иметь длину от {this.state.minInfoLength} до {this.state.maxInfoCount}</label>
        )
    }

    render()
    {
        if(this.state.userIsPushing)
        {
            return(
                <div className="main-content-block">
                    <div className="content-page">
                        <LoadingPage/>
                    </div>
                </div>
            )
        }
        if(this.state.userIsSuccessfulLoad)
        {
            return(
                <div className="main-content-block">
                    <div className="content-page">
                        <SuccessfulPage></SuccessfulPage>
                    </div>
                </div>
            )
        }
        if(this.props.User)
        {
            return(
                <div className="main-content-block">
                    <div className="content-page edit-user-page">
                        <h2>Редактировать информацию о себе</h2>
                        {this.renderNicknameLabel()}
                        <input type="text" defaultValue={this.props.User.Nickname}
                        onChange={this.onNicknameChange}></input>
                        <label>Изменить информацию о себе {this.state.Info}</label> 
                        <textarea className="input-description" type="text" defaultValue={this.props.User.Info}
                        onChange={this.onInfoChange}></textarea>
                        <RoundButton value="Сохранить изменения" onClick={this.onPushClick}></RoundButton>
                    </div>
                </div>
            )
        }
        else
        {
            return(
                <div className="main-content-block">
                    <div className="content-page">
                        <LoadingPage/>
                    </div>
                </div>
            )
        }
    }
}

export default EditUserPage;
