import React, {Component} from 'react'

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
        };
    }

    render()
    {
        console.log("render post page");
        return(
            <div className="main-content-block">
                <label>Название</label><input type="text"></input>
                <label>О посте</label><input type="text"></input>
            </div>
        );
    }
}

export default NewPostPage;
