import React, { Component } from "react";
import axios from "axios";
import { decorate, observable } from "mobx";
import { observer } from "mobx-react";
import "./Movie.scss";
import { Link } from "react-router-dom";

const Movie = observer(
  class Movie extends Component {
    path = require("../../Assets/Images/Movies/DefaultMovieImage.png");

    constructor(props) {
      super(props);
      this.getImage();
    }

    getImage = async () => {
      await axios
        .get(
          "https://localhost:44336/api/FileStorage?fileID=" +
            this.props.movie.FileID
        )
        .then((response) => {
          this.path = require("../../Assets/" + response.data);
        });
    };

    render() {
      return (
        <div className="base-container">
          <div>
            <Link to={"/movieInfoPage/" + this.props.movie.MovieID}>
              <img
                className="image"
                src={this.path}
                alt="movie"
                height="390px"
                width="260px"
              />
            </Link>
          </div>
          <div className="plot-outline">
            <p>{this.props.movie.PlotOutline}</p>
          </div>
        </div>
      );
    }
  }
);

decorate(Movie, {
  path: observable,
});

export default Movie;
