import React, { Component, Fragment } from "react";
import { Row, Card, CardBody, CardTitle, Table } from "reactstrap";
import IntlMessages from "../../helpers/IntlMessages";
import { Colxx, Separator } from "../../components/common/CustomBootstrap";
import Breadcrumb from "../../containers/navs/Breadcrumb";
import data from "../../locales/locale.json";
import { ROLES } from "../../constants/defaultValues";
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
import { L10n } from "@syncfusion/ej2-base";
import { NotificationManager } from "../../components/common/react-notifications";
import { getError } from "../../helpers/Utils";
import { getDataMaanager } from "../../helpers/Utils";

L10n.load(data);

export default class RolesPage extends Component {

  roles = getDataMaanager(ROLES);

  grid = null;
  wrapSettings = { wrapMode: "Content" };

  constructor(props) {
    super(props);

    this.toolbarOptions = [
      "Add",
      "Edit",
      "Delete",
      "Update",
      "Cancel"
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

    this.actionFailure = this.actionFailure.bind(this);
    this.actionComplete = this.actionComplete.bind(this);
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

  render() {
    return (
      <Fragment>
        <Row>
          <Colxx xxs="12">
            <Breadcrumb heading="menu.roles-page" match={this.props.match} />
            <Separator className="mb-5" />
          </Colxx>
        </Row>
        <Row>
          <Colxx xxs="12" className="mb-4">
            <Card className="mb-4">
              <CardBody>
                <CardTitle>
                  <IntlMessages id="menu.roles-page" />
                </CardTitle>
                <GridComponent
                  dataSource={this.roles}
                  locale="es-US"
                  allowPaging={true}
                  pageSettings={this.pageSettings}
                  toolbar={this.toolbarOptions}
                  editSettings={this.editSettings}
                  actionFailure={this.actionFailure}
                  actionComplete={this.actionComplete}
                  allowGrouping={true}
                  ref={(g) => (this.grid = g)}
                  query={this.query}
                  allowTextWrap={true}
                  textWrapSettings={this.wrapSettings}
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
                  </ColumnsDirective>
                  <Inject
                    services={[ForeignKey, Group, Page, Toolbar, Edit]}
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