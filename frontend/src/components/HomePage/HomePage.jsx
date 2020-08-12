import React, { Component } from "react";
import axios from "axios";
import Movies from "../Movie/Movies";
import { decorate, observable } from "mobx";
import { observer } from "mobx-react";
import './HomePage.scss'

const HomePage = observer(
  class HomePage extends Component {
    movies = [];

    componentDidMount() {
      axios.get("https://localhost:44336/api/Movie").then((response) => {
        this.movies = response.data;
      });
    }

    render() {
      let moviesProp = [];
      if (this.movies.length !== 0) {
        moviesProp = this.movies.m_Item2;
      }
      return (
        <div>
          <p><img src={require("./jw.jpg")} class="center" alt=""/></p>
          <Movies movies={moviesProp} />
        </div>
      );
    }
  }
);

decorate(HomePage, {
  movies: observable,
});

export default HomePage;
