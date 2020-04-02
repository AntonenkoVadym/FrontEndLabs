class Coach extends React.Component {
    constructor(props) {
        super(props);
        this.state = { data: props.coach };
        this.onClick = this.onClick.bind(this);
        this.link = "/Coaches/Edit/" + this.state.data.Id;
        this.flag = false;
    }
    onClick(e) {

        this.props.onRemove(this.state.data);
    }
    coachList() {
        return <tr>
            <td>{this.state.data.Name}</td>
            <td>{this.state.data.Age}</td>
            <td>{this.state.data.Sport}</td>
            <td>
                <a href={this.link} style={{ color: "cadetblue", textDecoration: "none" }}>Edit </a>

                <button style={{ color: "red" }} onClick={this.onClick} id="delete">Delete</button>
            </td>
        </tr>;
    }
    render() {
        return this.coachList();
    }
}

class CoachForm extends React.Component {

    constructor(props) {
        super(props);
        this.state = { name: "", age: "", sport: ""};


        this.onSubmit = this.onSubmit.bind(this);
        this.onChangeName = this.onChangeName.bind(this);
        this.onChangeAge = this.onChangeAge.bind(this);
        this.onChangeSport = this.onChangeSport.bind(this);
    }

    onChangeName(e) {

        this.setState({ name: e.target.value });
    }
    onChangeAge(e) {

        this.setState({ age: e.target.value });
    }
    onChangeSport(e) {

        this.setState({ sport: e.target.value });
    }
    onSubmit(e) {
        e.preventDefault();
        var coachName = this.state.name.trim();
        var coachAge = this.state.age.trim();
        var coachSport = this.state.sport.trim();


        if (!coachName) {
            return;
        }
        this.props.onCoachSubmit({ name: coachName, age: coachAge, sport: coachSport });
        console.log(this.props.onCoachSubmit);
        this.state = { name: "", age: "", sport: "" };
    }
    render() {
        return (
            <form onSubmit={this.onSubmit}>
                <p>
                    <input type="text"
                        placeholder="Name"
                        value={this.state.name}
                        onChange={this.onChangeName}
                        id="name" />

                </p>
                <p>
                    <input type="text"
                        placeholder="Age"
                        value={this.state.age}
                        onChange={this.onChangeAge}
                        id="age" />
                </p>
                <p>
                    <input type="text"
                        placeholder="Sport"
                        value={this.state.sport}
                        onChange={this.onChangeSport}
                        id="sport" />
                </p>

                <input type="submit" style={{ color: "green" }} value="Add a Coach" id="addCoach" />
            </form>
        );
    }
}

class CoachesList extends React.Component {

    constructor(props) {
        super(props);
        this.state = { coaches: [] };
        this.onRemoveCoach = this.onRemoveCoach.bind(this);
        this.onAddCoach = this.onAddCoach.bind(this);
    }

    loadData() {

        var xhr = new XMLHttpRequest();
        xhr.open("get", this.props.getUrl, true);

        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);

            this.setState({ coaches: data });
        }.bind(this);

        xhr.send();
    }
    componentDidMount() {
        this.loadData();
    }

    onAddCoach(coach) {

        if (coach) {
            console.log(coach);
            var data = new FormData();
            data.append("name", coach.name);
            data.append("age", parseInt(coach.age,10));
            data.append("sport", coach.sport);

            var xhr = new XMLHttpRequest();
            xhr.open("post", this.props.postUrl, true);

            xhr.onload = function () {
                if (xhr.status == 200) {
                    this.loadData();
                }
            }.bind(this);
            xhr.send(data);
        }
    }

    onRemoveCoach(coach) {

        if (coach) {
            var data = new FormData();
            data.append("id", coach.Id);
            var xhr = new XMLHttpRequest();
            xhr.open("delete", this.props.deleteUrl, true);
            xhr.onload = function () {
                if (xhr.status == 200) {
                    this.loadData();
                }
            }.bind(this);
            xhr.send(data);
        }
    }

    render() {

        var remove = this.onRemoveCoach;

        return <div>
            <CoachForm onCoachSubmit={this.onAddCoach} />
            <h2>List Coaches</h2>
            <div>
                <table className="table" >
                    <thead>
                        <tr>
                            <th><b>Name</b></th>
                            <th><b>Age</b></th>
                            <th><b>Sport</b></th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            this.state.coaches.map(function (coach) {
                                return <Coach key={coach.Id} coach={coach} onRemove={remove} />
                            })
                        }
                    </tbody>
                </table>
            </div>
        </div>;
    }
}

ReactDOM.render(
    <CoachesList getUrl="/Coaches/GetCoaches" deleteUrl="/Coaches/Delete" postUrl="/Coaches/Create" />,
    document.getElementById("content")
);