import React, { Component } from "react";
import axios from "axios";
import { decorate, observable } from "mobx";
import { observer } from "mobx-react";
import "./CastAndCrew.scss";

const CastAndCrew = observer(
    class CastAndCrew extends Component {
      path = require("../../Assets/Images/CastAndCrew/DefaultCastAndCrew.jpg");
  
      constructor(props) {
        super(props);
        this.getImage();
      }
  
      getImage = async () => {
        await axios
          .get(
            "https://localhost:44336/api/FileStorage?fileID=" +
              this.props.castAndCrew.FileID
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
                  className="image_cc"
                  src={this.path}
                  alt="CastAndCrew"
                  height="220px"
                  width="220px"
                />
            </div>
            <div>
              <p>{this.props.castAndCrew.FirstName}</p>
              <p>{this.props.castAndCrew.LastName}</p>
            </div>
          </div>
        );
      }
    }
  );
  
  decorate(CastAndCrew, {
    path: observable,
  });
  
  export default CastAndCrew;
  