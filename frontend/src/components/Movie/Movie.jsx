import React, { Component } from "react";
import axios from "axios";
import { decorate, observable } from "mobx";
import { observer } from "mobx-react";
import "./Movie.scss";

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
            this.props.movie.FileID +"&pageSize=5"
        )
        .then((response) => {
          console.log(response.data);
          this.path = require("../../Assets/" + response.data);
        });
    };

    render() {
      return (
        <div className="base-container">
          <div className="title">
            <p>{this.props.movie.Title}</p>
          </div>
          <div>
            <img className="image" src={this.path} alt="movie" height="390px" width="280px" />;
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