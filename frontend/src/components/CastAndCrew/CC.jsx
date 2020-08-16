import React, { Component } from "react";
import CastAndCrew from "./CastAndCrew";
import "./CastAndCrew.scss";

class Cast extends Component {
  render() {
    let cast = this.props.cast.map((castAndCrew) => {
      return <CastAndCrew key={castAndCrew.CastID} castAndCrew={castAndCrew} />;
    });
    return <div className="CastDiv">{cast}</div>;
  }
}

export default Cast;
