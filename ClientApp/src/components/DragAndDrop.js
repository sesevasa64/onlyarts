import React, {Component}from 'react'

import './css/DragAndDrop.css'

class DragAndDrop extends Component
{
    constructor(props)
    {
        super(props);
        this.state = {
            files_str: "Файлы: ",
            file_ext: /^.*\.(jpe?g|gif|png|tiff)$/i,
            added_files: [],
        }
        this.dropHandler = this.dropHandler.bind(this);
    }

    dragOverHandler(ev) {
        ev.preventDefault();
    }

    dropHandler(ev)
    {

        ev.preventDefault();
        if (ev.dataTransfer.items) {
            let files_str = this.state.files_str;
            let added_files = [];
            
            for (var i = 0; i < ev.dataTransfer.items.length; i++) {
            
            if (ev.dataTransfer.items[i].kind === 'file') {
                var file = ev.dataTransfer.items[i].getAsFile();
                if(this.state.file_ext.test(file.name))
                {
                    files_str += file.name;
                    added_files.push(file);
                    this.props.addFiles(file)
                }
            }
            this.setState({
                files_str: files_str,
            })
        }
        } else {
            
            for (var i = 0; i < ev.dataTransfer.files.length; i++) {
                //console.log('... file[' + i + '].name = ' + ev.dataTransfer.files[i].name);
            }
        }
    }

    render()
    {
        return(
            <div id="drop_zone" onDrop={this.dropHandler} onDragOver={this.dragOverHandler}>
                <p>Добавьте изображени</p>
            </div>
        )
    }
}

export default DragAndDrop;
