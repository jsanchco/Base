import React, { Component, Fragment } from "react";
import { Row, Card, CardBody, CardTitle } from "reactstrap";
import IntlMessages from "../../../helpers/IntlMessages";
import { Colxx, Separator } from "../../../components/common/CustomBootstrap";
import Breadcrumb from "../../../containers/navs/Breadcrumb";
import { USERS, ROLES } from "../../../constants/defaultValues";
import {
  ColumnDirective,
  ColumnsDirective,
  GridComponent,
  Edit,
  Inject,
  Toolbar,
  Page,
  ForeignKey,
  Group,
  Sort,
} from "@syncfusion/ej2-react-grids";
import { NotificationManager } from "../../../components/common/react-notifications";
import { getError } from "../../../helpers/Utils";
import { getDataManager } from "../../../helpers/Utils";

export default class Users extends Component {

  users = getDataManager(USERS);
  roles = getDataManager(ROLES);

  roleIdRules = { required: true };
  grid = null;
  wrapSettings = { wrapMode: "Content" };

  constructor(props) {
    super(props);

    this.state = {
      rowSelected: null,
    };

    this.toolbarOptions = [
      "Add",
      "Edit",
      "Delete",
      "Update",
      "Cancel",
      {
        text: "Detalles",
        tooltipText: "Detalles",
        prefixIcon: "e-custom-icons e-details",
        id: "Details",
      },
      "Search",
    ];
    this.editSettings = {
      showDeleteConfirmDialog: true,
      allowEditing: true,
      allowAdding: true,
      allowDeleting: true,
      newRowPosition: "Top",
    };
    this.pageSettings = { pageCount: 10, pageSize: 10 };
    this.customAttributes = { class: "customcss" };
    this.numericParams = {
      params: {
        decimals: 2,
        format: "C",
        validateDecimalOnType: true,
        currency: "EUR"
      }
    };

    this.actionFailure = this.actionFailure.bind(this);
    this.actionComplete = this.actionComplete.bind(this);
    this.clickHandler = this.clickHandler.bind(this);
    this.rowSelected = this.rowSelected.bind(this);

    this.format = { type: "dateTime", format: "dd/MM/yyyy" };
  }

  actionFailure(args) {
    const error = getError(args);
    NotificationManager.error(
      error.text,
      `Status: ${error.status}`,
      3000,
      null,
      null,
      "filled"
    );
  }

  actionComplete(args) {
    if (args.requestType === "save") {
      NotificationManager.success(
        "Operación realizada con éxito",
        "",
        3000,
        null,
        null,
        "filled"
      );
      this.setState({ rowSelected: null });
    }
    if (args.requestType === "delete") {
      NotificationManager.success(
        "Operación realizada con éxito",
        "",
        3000,
        null,
        null,
        "filled"
      );
      this.setState({ rowSelected: null });
    }
  }

  clickHandler(args) {
    if (args.item.id === "Details") {
      const { rowSelected } = this.state;
      if (rowSelected !== null) {
        this.props.history.push({
          pathname: `/app/user-page/${rowSelected.id}`,
          state: {
            user: rowSelected,
          },
        });
      } else {
        NotificationManager.error(
          "Debes seleccionar un Usuario",
          `Status: -1`,
          3000,
          null,
          null,
          "filled"
        );
      }
    }
  }

  rowSelected() {
    const selectedRecords = this.grid.getSelectedRecords();
    this.setState({ rowSelected: selectedRecords[0] });
  }

  render() {
    return (
      <Fragment>
        <Row>
          <Colxx xxs="12">
            <Breadcrumb heading="menu.users-page" match={this.props.match} />
            <Separator className="mb-5" />
          </Colxx>
        </Row>
        <Row>
          <Colxx xxs="12" className="mb-4">
            <Card className="mb-4">
              <CardBody>
                <CardTitle>
                  <IntlMessages id="menu.users-page" />
                </CardTitle>

                <GridComponent
                  dataSource={this.users}
                  locale="es"
                  allowPaging={true}
                  pageSettings={this.pageSettings}
                  toolbar={this.toolbarOptions}
                  toolbarClick={this.clickHandler}
                  editSettings={this.editSettings}
                  actionFailure={this.actionFailure}
                  actionComplete={this.actionComplete}
                  allowGrouping={true}
                  rowSelected={this.rowSelected}
                  ref={(g) => (this.grid = g)}
                  query={this.query}
                  allowTextWrap={true}
                  textWrapSettings={this.wrapSettings}
                  allowSorting={true}
                >
                  <ColumnsDirective>
                    <ColumnDirective
                      field="id"
                      headerText="Id"
                      width="40"
                      isPrimaryKey={true}
                      isIdentity={true}
                      visible={false}
                    />
                    <ColumnDirective
                      field="name"
                      headerText="Nombre"
                      width="100"
                      headerTextAlign="center"
                      customAttributes={this.customAttributes}
                    />
                    <ColumnDirective
                      field="surname"
                      headerText="Apellidos"
                      width="100"
                      headerTextAlign="center"
                      customAttributes={this.customAttributes}
                    />
                    <ColumnDirective
                      field="birthdate"
                      headerText="Fecha Nacimiento"
                      width="100"
                      type="date"
                      format={this.format}
                      editType="datepickeredit"
                      headerTextAlign="center"
                      textAlign="center"
                      customAttributes={this.customAttributes}
                    />
                    <ColumnDirective 
                      field="salary"
                      headerText="Salario" 
                      width="100" 
                      format="C2"
                      // currency="EUR"
                      textAlign="Right"
                      editType="numericedit"
                      edit={this.numericParams}
                      headerTextAlign="center"
                      customAttributes={this.customAttributes}
                    />
                    <ColumnDirective
                      field="roleId"
                      headerText="Role"
                      width="100"
                      editType="dropdownedit"
                      foreignKeyValue="name"
                      foreignKeyField="id"
                      validationRules={this.roleIdRules}
                      dataSource={this.roles}
                      headerTextAlign="center"
                      customAttributes={this.customAttributes}
                    />
                  </ColumnsDirective>
                  <Inject
                    services={[ForeignKey, Group, Page, Toolbar, Edit, Sort]}
                  />
                </GridComponent>
              </CardBody>
            </Card>
          </Colxx>
        </Row>
      </Fragment>
    )
  }
}