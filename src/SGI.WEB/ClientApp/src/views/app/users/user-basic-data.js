import React, { Component, Fragment } from "react";
import { Row, Card, CardBody, CardTitle, FormGroup, Label, Button } from "reactstrap";
import { Formik, Form, Field } from "formik";
import IntlMessages from "../../../helpers/IntlMessages";
import { Colxx, Separator } from "../../../components/common/CustomBootstrap";
import Breadcrumb from "../../../containers/navs/Breadcrumb";
import {
  createSpinner,
  showSpinner,
  hideSpinner,
} from "@syncfusion/ej2-popups";
import { DatePickerComponent } from "@syncfusion/ej2-react-calendars";
import { NumericTextBoxComponent } from '@syncfusion/ej2-react-inputs';
import axios from "../../../helpers/axios";
import { catchError } from "../../../helpers/Utils";
import { NotificationManager } from "../../../components/common/react-notifications";

export default class UserBasicData extends Component {
  constructor(props) {
    super(props);

    this.state = {
      user: null,
      userId: this.props.match.params.id,
      roleId: null,
      username: null,
      name: null,
      surname: null,
      birthdate: null,
      salary: 0
    };

    this.handleInputChange = this.handleInputChange.bind(this);
    this.handleDate = this.handleDate.bind(this);
    this.handleSalary = this.handleSalary.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.getUser = this.getUser.bind(this);
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
          username: result.data.username,
          birthdate: result.data.birthdate,  
          salary: result.data.salary,
          roleId: result.data.roleId
        })

        hideSpinner(element);
      })
      .catch(error => {
        catchError(error);

        hideSpinner(element);
      });
  }

  handleInputChange(event) {
    const target = event.target;
    const name = target.name;

    this.setState({
      [name]: target.value
    });
  }

  handleDate(event) {
    const name = event.element.name;
    this.setState({
      [name]: event.value,
    });
  }

  handleSalary(args) {
    this.setState({ salary: args.value });
  }

  handleSubmit(e) {
    const element = document.getElementById("user-card");

    createSpinner({
      target: element,
    });
    showSpinner(element);

    const url = `/api/Users`;
    const oldUser = this.getUser(this.state.user);
    const newUser = this.getUser(this.state);
    const userPatch = this.getPatchData(oldUser, newUser);
    if (userPatch.length === 0) {
      NotificationManager.error(
        "Los datos coinciden, debes cambiar algún valor para poder Guardar",
        "",
        3000,
        null,
        null,
        "filled"
      );
      hideSpinner(element);
      return;
    }

    const userId = this.state.userId;
    axios.patch(
      url,
      userPatch,
      {
        params: {
          userId
        }
      })
      .then(result => {
        if (result.status === 201) {
          NotificationManager.success(
            "Operación realizada con éxito",
            "",
            3000,
            null,
            null,
            "filled"
          );
          this.setState({ user: result.data });
        }
        hideSpinner(element);
      })
      .catch(error => {
        catchError(error);
        hideSpinner(element);
      });
  }

  getUser(user) {
    return {
      id: this.state.userId,
      roleId: this.state.roleId,
      username: this.state.username,
      name: user.name,
      surname: user.surname,
      salary: user.salary,
      birthdate: Date.parse(user.birthdate)
    };
  }

  getPatchData(oldUser, newUser) {
    var rfc6902 = require("rfc6902");
    const operations = rfc6902.createPatch(oldUser, newUser);
    operations.forEach(item => {
      if (item.path === "/birthdate") {
        item.value = new Date(item.value);
      }
    });

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
                  onSubmit={this.handleSubmit}
                >
                  {() => (
                    <Form className="av-tooltip tooltip-label-right">
                      <Row>
                        <Colxx xxs="3">
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
                        <Colxx xxs="3">
                          <FormGroup>
                            <Label><IntlMessages id="user-page.surname" /></Label>
                            <Field
                              className="form-control"
                              name="surname"
                              id="surname"
                              locale="es-US"
                              required
                              value={this.state.surname || ""}
                              onChange={this.handleInputChange}
                            />
                          </FormGroup>
                        </Colxx>
                        <Colxx xxs="3">
                          <FormGroup>
                            <Label><IntlMessages id="user-page.birthdate" /></Label>
                            <DatePickerComponent
                              id="birthdate"
                              name="birthdate"
                              required
                              format="dd/MM/yyyy"
                              value={this.state.birthdate || ""}
                              change={this.handleDate}
                              style={{ marginTop: "10px" }}
                              timeZone={false}
                              locale="es"
                            />
                          </FormGroup>
                        </Colxx>
                        <Colxx xxs="3">
                          <FormGroup>
                            <Label><IntlMessages id="user-page.salary" /></Label>
                            <NumericTextBoxComponent
                              id="salary"
                              name="salary"
                              locale="es"
                              currency="EUR"
                              format="c2"
                              style={{ marginTop: "5px" }}
                              value={this.state.salary}
                              change={this.handleSalary}
                            />
                          </FormGroup>
                        </Colxx>
                      </Row>
                      <Row style={{ marginTop: "10px" }}>
                        <Button color="primary" type="submit" >
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
