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
            this.props.movie.FileID
        )
        .then((response) => {
          console.log(response.data);
          this.path = require("../../Assets/" + response.data);
        });
    };

    render() {
      return (
        <div className="base-container">
          <div>
            <img
              className="image"
              src={this.path}
              alt="movie"
              height="390px"
              width="280px"
            />
            ;
          </div>
          <div>
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
