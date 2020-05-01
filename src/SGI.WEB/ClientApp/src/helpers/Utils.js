import { defaultDirection } from "../constants/defaultValues";
import { DataManager, WebApiAdaptor } from "@syncfusion/ej2-data";
import { config } from "../constants/defaultValues";

export const mapOrder = (array, order, key) => {
  array.sort(function (a, b) {
    var A = a[key], B = b[key];
    if (order.indexOf(A + "") > order.indexOf(B + "")) {
      return 1;
    } else {
      return -1;
    }
  });
  return array;
};


export const getDateWithFormat = () => {
  const today = new Date();
  let dd = today.getDate();
  let mm = today.getMonth() + 1; //January is 0!

  var yyyy = today.getFullYear();
  if (dd < 10) {
    dd = '0' + dd;
  }
  if (mm < 10) {
    mm = '0' + mm;
  }
  return dd + '.' + mm + '.' + yyyy;
}

export const getCurrentTime = () => {
  const now = new Date();
  return now.getHours() + ":" + now.getMinutes()
}


export const getDirection = () => {
  let direction = defaultDirection;
  if (localStorage.getItem("direction")) {
    const localValue = localStorage.getItem("direction");
    if (localValue === "rtl" || localValue === "ltr") {
      direction = localValue;
    }
  }
  return {
    direction,
    isRtl: direction === "rtl"
  };
};

export const setDirection = localValue => {
  let direction = "ltr";
  if (localValue === "rtl" || localValue === "ltr") {
    direction = localValue;
  }
  localStorage.setItem("direction", direction);
};

export const getError = (args) => {
  console.log("error ->", args);
  if (args.error && args.error.error) {
    return {
      status: args.error.error.status,
      text: args.error.error.responseText === "" ? "unknown" : args.error.error.responseText
    };
  }

  if (Array.isArray(args) && args[0]) {
    return {
      status: args[0].error.status,
      text: args[0].error.responseText === "" ? "unknown" : args[0].error.responseText
    };
  }

  if (args.error && args.error.message) {
    return {
      status: -1,
      text: args.error.message
    };
  }

  return { status: -1, text: "unknown" }
};

export const getDataMaanager = urlApi => {
  return (
    new DataManager({
      adaptor: new WebApiAdaptor(),
      url: `${config.URL_API}/${urlApi}`,
      headers: [{ Authorization: "Bearer " + localStorage.getItem('jwt') }]
    })
  );
};

