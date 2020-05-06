import React, { Component, Fragment } from "react";
import { Row, Card, CardBody, CardTitle, FormGroup, Label, Button } from "reactstrap";
import { Formik, Form, Field } from "formik";
import IntlMessages from "../../helpers/IntlMessages";
import { Colxx, Separator } from "../../components/common/CustomBootstrap";
import Breadcrumb from "../../containers/navs/Breadcrumb";
import {
  createSpinner,
  showSpinner,
  hideSpinner,
} from "@syncfusion/ej2-popups";
import { DatePickerComponent } from "@syncfusion/ej2-react-calendars";
import axios from "../../helpers/axios";
import { catchError } from "../../helpers/Utils";
import { applyPatch } from 'rfc6902';

export default class UserPage extends Component {
  constructor(props) {
    super(props);

    this.state = {
      user: null,
      userId: this.props.match.params.id,
      name: null,
      surname: null,
      birthdate: null
    };

    this.handleInputChange = this.handleInputChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.getOldUser = this.getOldUser.bind(this);
    this.getNewUser = this.getNewUser.bind(this);
    this.getPatchData = this.getPatchData.bind(this);
  }

  componentDidMount() {
    const element = document.getElementById("user-card");

    createSpinner({
      target: element,
    });
    showSpinner(element);

    const url = `/api/Users/${this.state.userId}`
    axios.get(url)
      .then(result => {
        this.setState({
          user: result.data,
          name: result.data.name,
          surname: result.data.surname,
          birthdate: this.parseDate(result.data.birthdate)
        })

        hideSpinner(element);
      })
      .catch(error => {
        catchError(error);

        hideSpinner(element);
      });
  }

  parseDate(args) {
    if (args === null || args === undefined || args === "") {
      return "";
    }

    return `${args.substring(3, 5)}/${args.substring(0, 2)}/${args.substring(
      6,
      10
    )}`;
  }

  handleInputChange(event) {
    const target = event.target;
    const name = target.name;

    this.setState({
      [name]: target.value
    });
  }

  handleSubmit() {
    const element = document.getElementById("user-card");

    createSpinner({
      target: element,
    });
    showSpinner(element);

    debugger;
    const url = `/api/Users`;
    const user = this.getPatchData();
    const userId = this.state.userId;
    axios.patch(
      url, 
      user, 
      { params: {
        userId
      }})
      .then(result => {
        console.log("result ->", result);
        hideSpinner(element);
      })
      .catch(error => {
        catchError(error);
        hideSpinner(element);
      });
  }

  getOldUser() {
    debugger;
    return {
      id: this.state.userId,
      name: this.state.user.name,
      surname: this.state.user.surname,
      birthdate: this.state.user.birthdate,
      roleId: this.state.user.roleId
    };
  }

  getNewUser() {
    debugger;
    return {
      id: this.state.userId,
      name: this.state.name,
      surname: this.state.surname,
      birthdate: this.parseDate(this.state.birthdate),
      roleId: this.state.user.roleId
    };
  }

  getPatchData() {
    var rfc6902 = require("rfc6902");
    const operations = rfc6902.createPatch(this.getOldUser(), this.getNewUser());

    return operations;
  }

  render() {
    const fullName = this.state.user ?
      `${this.state.user.name} ${this.state.user.surname}` :
      "";

    return (
      <Fragment>
        <Row>
          <Colxx xxs="12">
            <Breadcrumb heading="menu.user-page" match={this.props.match} />
            <Separator className="mb-5" />
          </Colxx>
        </Row>
        <Row>
          <Colxx xxs="12" className="mb-4">
            <Card className="mb-4" id="user-card">
              <CardBody>
                <CardTitle>
                  {fullName}
                </CardTitle>

                <Formik
                  onSubmit={this.handleSubmit}>
                  {() => (
                    <Form className="av-tooltip tooltip-label-right">
                      <Row>
                        <Colxx xxs="4">
                          <FormGroup>
                            <Label><IntlMessages id="user-page.name" /></Label>
                            <Field
                              className="form-control"
                              name="name"
                              id="name"
                              required
                              value={this.state.name || ""}
                              onChange={this.handleInputChange}
                            />
                          </FormGroup>
                        </Colxx>
                        <Colxx xxs="4">
                          <FormGroup>
                            <Label><IntlMessages id="user-page.surname" /></Label>
                            <Field
                              className="form-control"
                              name="surname"
                              id="surname"
                              required
                              value={this.state.surname || ""}
                              onChange={this.handleInputChange}
                            />
                          </FormGroup>
                        </Colxx>
                        <Colxx xxs="4">
                          <FormGroup>
                            <Label><IntlMessages id="user-page.birthdate" /></Label>
                            <DatePickerComponent
                              id="birthdate"
                              name="birthdate"
                              required
                              format="dd/MM/yyyy"
                              value={this.state.birthdate || ""}
                              onChange={this.handleInputChange}
                              style={{ marginTop: "10px" }}
                            />
                          </FormGroup>
                        </Colxx>
                      </Row>
                      <Row style={{ marginTop: "10px" }}>
                        <Button color="primary" type="submit">
                          <IntlMessages id="common.save" />
                        </Button>
                      </Row>
                    </Form>
                  )}
                </Formik>

              </CardBody>
            </Card>
          </Colxx>
        </Row>
      </Fragment>
    )
  }
}
