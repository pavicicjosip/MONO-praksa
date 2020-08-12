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
          <p><img src={require("./b.png")} class="center" alt=""/></p>
          <h1 className='centerd'>Welcome...</h1>
          <h3 className='centerd1'>You've found the most accurate source for TV and film. Our information comes from fans like you, so create a free account and help your favorite shows and movies.</h3>
          <h2 className='title'>Featured Movies</h2>
          <div className='spacer'></div>
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
