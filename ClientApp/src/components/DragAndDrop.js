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
        console.log('File(s) in drop zone');
      
        // Prevent default behavior (Prevent file from being opened)
        ev.preventDefault();
    }

    dropHandler(ev)
    {
        console.log('File(s) dropped');

        ev.preventDefault();
        if (ev.dataTransfer.items) {
            let files_str = this.state.files_str;
            let added_files = [];
            // Use DataTransferItemList interface to access the file(s)
            for (var i = 0; i < ev.dataTransfer.items.length; i++) {
            // If dropped items aren't files, reject them
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
            // Use DataTransfer interface to access the file(s)
            for (var i = 0; i < ev.dataTransfer.files.length; i++) {
                console.log('... file[' + i + '].name = ' + ev.dataTransfer.files[i].name);
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
