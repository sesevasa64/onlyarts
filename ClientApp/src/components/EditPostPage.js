import React, {Component} from "react";
import LoadingPage from "./LoadingPage";

class EditPostPage extends Component
{
    constructor(props)
    {
        super(props)
    }

    render()
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

export default EditPostPage;
