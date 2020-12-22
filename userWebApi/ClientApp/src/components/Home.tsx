import React, { Component } from 'react';
import Button from 'react-bootstrap/Button';

class Home extends Component {
    constructor(props) {
        super(props);
        this.state = {
            fileContent: 'empty',
            fileName: 'ss'
        };
        this.handleTextChange = this.handleTextChange.bind(this);
    }
    handleTextChange(event) {
        console.clear();
        this.setState({ [event.target.name]: event.target.value });
        // console.log(this.state);
    }
    handleOpenFile(event) {
        console.log('open file');
    }
    handleDeleteFile(event) {
        console.log('delete file');
    }
    handleEditFile(event) {
        console.log('Edit file');
    }
    handleEditFileContent(event) {
        console.log('Edit file content');
    }
    handleSetSymbols(event) {
        console.log('Set symbols');
    }
    render() {
        return (
            <div>
                <div>
                    <h1>Создание и удаление файла</h1>
                    <input
                        type="text"
                        name="fileName"
                        value={this.state.fileName}
                        onChange={this.handleTextChange}
                    />
          .txt
          <br></br>
                    <button name="open" onClick={this.handleOpenFile}>
                        Open
          </button>
                    <button name="delete" onClick={this.handleDeleteFile}>
                        Delete
          </button>
                    <button name="editFile" onClick={this.handleEditFile}>
                        Edit
          </button>
                </div>
                <br />
                <br />
                <div>
                    <h1>Изменение содержимого файла</h1>
                    <textarea
                        name="fileContent"
                        value={this.state.fileContent}
                        cols={100}
                        rows={15}
                        onChange={this.handleTextChange}
                    />
                </div>
                <Button name="editFileContent" onClick={this.handleEditFileContent}>
                    Edit
        </Button>
                <Button name="setSymbols" onClick={this.handleSetSymbols}>
                    set symbols
        </Button>
            </div>
        );
    }
}

export default Home;
