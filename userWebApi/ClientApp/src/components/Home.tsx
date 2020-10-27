import * as React from 'react';

enum Education {
    High = "High",
    Partly = "Partly",
    Middle = "Middle"
};

class Home extends React.Component {
    constructor(props) {
        super(props);
        this.state = { name: '', isMale: true, education: Education.High, hasCar: false, messages: [] };
        this.handleHasCar = this.handleHasCar.bind(this);
    }

    handleChange = (event) => {
        this.setState({ [event.target.name]: event.target.value });
    }

    handleBoolChange = (event) => {
        this.setState({ [event.target.name]: event.target.value !== "false" });
    }

    handleHasCar = (event) => {
        let isChecked = event.target.checked;
        this.setState({ hasCar: isChecked });
    }

    handleReset = (event) => {
        this.setState( { name: '', isMale: true, education: Education.High, hasCar: false, messages: [] });

    }
    
    handleSubmit = (event) => {
        var that = this;
        fetch('api/UserRegister', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            // We convert the React state to JSON and send it as the POST body
            body: JSON.stringify(this.state)
        }).then(function (response) {
            console.log(response);
            response.json().then(function (json) {
                that.setState({ messages: !response.ok ? [`status: ${response.statusText}`] : json.messages });
            });
        });

        event.preventDefault();
    }

    render() {
        return (
            <div>
                <form onSubmit={this.handleSubmit} onReset = {this.handleReset}>
                    <h1> Заполните анкету:<br /></h1>
                    <h2> Введите фамилию, имя:<br /></h2>
                    <input type='text' name='name' value={this.state.name} onChange={this.handleChange} /><br />
                    <h3> Укажите пол:<br /></h3>
                    <input type="RADIO" checked={this.state.isMale} name="isMale" value="true" onChange={this.handleBoolChange} /> Мужской<br />
                    <input type="RADIO" checked={!this.state.isMale} name="isMale" value="false" onChange={this.handleBoolChange}/> Женский
                    <h3>  Укажите образование:<br /></h3>
                    <select name="education"
                        value={this.state.education}
                        onChange={this.handleChange}>
                        <option value={Education.High}>Высшее</option>
                        <option value={Education.Partly}>Незаконченное высшее</option>
                        <option value={Education.Middle}>Среднее полное</option>
                    </select>
                    <h3>  Наличие авто:<br /></h3>
                    <input type="checkbox" onClick={this.handleHasCar}  name = "hasCar"/>

                    <div><p>
                        <input type="submit" value="Отправить" />
                        <input type="reset" value="Сброс" /> <br />
                    </p></div>
                </form>
                <div>
                    <ul>
                        {this.state.messages.map(x => (<li>{x}</li>))}
                    </ul>

                </div>

            </div>
        );
    }
}

export default Home;
