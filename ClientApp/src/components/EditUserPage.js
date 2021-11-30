import React, {Component} from "react";
import './css/EditUserPage.css'
import LoadingPage from "./LoadingPage";
import RoundButton from "./RoundButton";

class EditUserPage extends Component
{
    constructor(props)
    {
        super(props)
    }

    render()
    {
        if(this.props.User)
        {
            return(
                <div className="main-content-block">
                    <div className="content-page edit-user-page">
                        <h2>Редактировать информацию о себе</h2>
                        <label>Изменить Nickname</label>
                        <input type="text" value={this.props.User.Login}></input>
                        <label>Изменить информацию о себе</label>
                        <textarea className="input-description" type="text" value={this.props.User.Info}></textarea>
                        <RoundButton value="Сохранить изменения"></RoundButton>
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
