import React, { Component } from "react";
import "./MovieInfoPage.scss";
import { decorate, observable } from "mobx";
import { observer } from "mobx-react";
//import axios from "axios";

const MovieInfoPage = observer(
    class MovieInfoPage extends Component {


        render() {
            console.log(this.props);
            return(
                <div>
                   <h1 className="Proba">ASno</h1>
                </div>
            );
        }
    }
);

decorate(MovieInfoPage, {
    path: observable,
  });

export default MovieInfoPage;