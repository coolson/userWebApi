import * as React from 'react';
import "../custom.css"; //C:\dev\react_laba2\userWebApi\ClientApp\src\custom.css

//Module not found: Can't resolve './/custom.css' in 'C:\dev\react_laba2\userWebApi\ClientApp\src\components'

enum Education {
    High = "High",
    Partly = "Partly",
    Middle = "Middle"
};
function TestLogin(login) {
    if (login === "") { alert('Пустой логин'); return false; }
    if (/^[a-zA-Z1-9]+$/.test(login) === false) { alert('В логине должны быть только латинские буквы'); return false; }
    if (login.length < 4 || login.length > 20) { alert('В логине должен быть от 4 до 20 символов'); return false; }
    if (parseInt(login.substr(0, 1))) { alert('Логине должен начинаться с буквы'); return false; }

    return true;
}

function TestPassword(login) {
    if (login === "") { alert('Пустой пароль'); return false; }
    if (login.length < 4 || login.length > 20) { alert('В пароле должно быть от 4 до 20 символов'); return false; }

    return true;
}



class Home extends React.Component {
    constructor(props) {
        super(props);
        this.state = { name: '', password: '', isMale: true, education: Education.High, hasCar: false, messages: [] };
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
        this.setState({ name: '', password: '', isMale: true, education: Education.High, hasCar: false, messages: [] });

    }

    handleSubmit = (event) => {
        if (TestLogin(this.state.name) && TestPassword(this.state.password)) {
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
        }
        event.preventDefault();
    }

    render() {
        return (
            <div>
                <form onSubmit={this.handleSubmit} onReset={this.handleReset}>
                    <h1 className="b" > Заполните анкету: <br /></h1>
                    <h2> login:<br /></h2>
                    <input type='text' name='name' value={this.state.name}  onChange={this.handleChange} /><br />



                    <h2> password:<br /></h2>
                    <input type='password' name='password' value={this.state.password} onChange={this.handleChange} className="passwordInput"/><br />



                    <h3> Укажите пол:<br /></h3>
                    <input type="RADIO" checked={this.state.isMale} name="isMale" value="true" onChange={this.handleBoolChange} /> Мужской<br />
                    <input type="RADIO" checked={!this.state.isMale} name="isMale" value="false" onChange={this.handleBoolChange} /> Женский
                    <h3>  Укажите образование:<br /></h3>
                    <select name="education"
                        value={this.state.education}
                        onChange={this.handleChange}>
                        <option value={Education.High}>Высшее</option>
                        <option value={Education.Partly}>Незаконченное высшее</option>
                        <option value={Education.Middle}>Среднее полное</option>
                    </select>
                    <h3>  Наличие авто:<br /></h3>
                    <input type="checkbox"  onClick={this.handleHasCar} name="hasCar" />

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
