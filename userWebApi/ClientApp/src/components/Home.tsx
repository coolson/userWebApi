import React, { Component } from 'react';
//import Button from 'react-bootstrap/Button';

class Home extends Component {
    constructor(props) {
        super(props);
        this.state = {
            fileContent: '',
            fileName: 'my-lovely-file'
        };
        this.handleTextChange = this.handleTextChange.bind(this);
        this.handleOpenFile = this.handleOpenFile.bind(this);
        this.handleDeleteFile = this.handleDeleteFile.bind(this);
        this.handleUpdateFile = this.handleUpdateFile.bind(this);
        this.handleEditFileContent = this.handleEditFileContent.bind(this);
        this.handleSetSymbols = this.handleSetSymbols.bind(this);
    }
    handleTextChange(event) {
        console.clear();
        this.setState({ [event.target.name]: event.target.value });
        // console.log(this.state);
    }
    handleOpenFile(event) {
        fetch(`http://localhost:51001/api/datafile?filename=${this.state.fileName}`) //GET
            .then(response => response.text())
            .then(content => this.setState({ fileContent: content })
            );
    }
    handleDeleteFile(event) {
        fetch(`http://localhost:51001/api/datafile?filename=${this.state.fileName}`
            , { method: 'DELETE' })
            .then(response => response.status)
            .then(status => {
                console.log(status);
                this.setState({ fileContent: "" });
            });
    }

    handleUpdateFile(event) {
        fetch(`http://localhost:51001/api/datafile?filename=${this.state.fileName}`
            , {
                method: 'POST',
                body: JSON.stringify(this.state),
                headers: { 'Content-Type': 'application/json; charset=utf-8' }
            })
            .then(response => response.status)
            .then(status => {
                console.log(status);
            });
    }

    handleEditFileContent(event) {
        console.log('Edit file content');
    }
    handleSetSymbols(event) {
        fetch(`http://localhost:51001/api/datafile/setsymbols?filename=${this.state.fileName}`
            , {
                method: 'POST',
                body: JSON.stringify(this.state),
                headers: { 'Content-Type': 'application/json; charset=utf-8' }
            })
            .then(response => response.text())
            .then(content => this.setState({ fileContent: content })
            );
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
                        Open or Create
          </button>
                    <button name="delete" onClick={this.handleDeleteFile}>
                        Delete
          </button>
                    <button name="editFile" onClick={this.handleUpdateFile}>
                        Save
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
                
                <button name="setSymbols" onClick={this.handleSetSymbols}>
                    set symbols
                </button>
            </div>
        );
    }
}

export default Home;
