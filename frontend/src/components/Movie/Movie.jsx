import React, { Component } from "react";
import axios from "axios";
import { decorate, observable } from "mobx";
import { observer } from "mobx-react";
import "./Movie.scss";
import { Link } from "react-router-dom";

const Movie = observer(
  class Movie extends Component {
    path = require("../../Assets/Images/Movies/DefaultMovieImage.png");
    addMovieFlag = true;
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

    buttonFunction = (event) => {
      event.preventDefault();
      if (this.addMovieFlag) {
        this.props.button(this.props.movie.MovieID);
      }
    };

    render() {
      let buttonTitle = "";
      if (this.props.buttonTitle) {
        buttonTitle = this.props.buttonTitle;
      }
      if (this.props.addedMovies) {
        if (this.props.addedMovies.length !== 0) {
          this.props.addedMovies.map((movieID) => {
            if (this.props.movie.MovieID === movieID) {
              this.addMovieFlag = false;
              buttonTitle = "Added";
              return true;
            }
            return false;
          });
        }
      }
      return (
        <div className="base-container">
          {this.props.buttonTitle ? (
            <button onClick={this.buttonFunction}>{buttonTitle}</button>
          ) : null}
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
