import React, { Component } from "react";
import "./MovieInfoPage.scss";
import { decorate, observable } from "mobx";
import { observer } from "mobx-react";
import axios from "axios";
import Movie from "../Movie/Movie"

const MovieInfoPage = observer(
    class MovieInfoPage extends Component {
        movie = {}


        componentDidMount() {
            axios.get("https://localhost:44336/api/Movie/" + this.props.match.params.id)
            .then((response) => this.movie = response.data);
        }

        render() {
            console.log(this.movie.Title);
            return(
                <div className="base-container">
                   <Movie key={this.movie.MovieID} movie={this.movie} />;
                </div>
            );
        }
    }
);

decorate(MovieInfoPage, {
    movie: observable,
});

export default MovieInfoPage;