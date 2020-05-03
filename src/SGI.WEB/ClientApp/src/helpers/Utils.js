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
      text: args.error.error.responseText === "" ? getTextError(args.error.error.status) : args.error.error.responseText
    };
  }

  if (Array.isArray(args) && args[0]) {
    return {
      status: args[0].error.status,
      text: args[0].error.responseText === "" ? getTextError(args[0].error.status) : args[0].error.responseText
    };
  }

  if (args.error && args.error.message) {
    return {
      status: -1,
      text: args.error.message
    };
  }

  return { status: -1, text: "Unknown" }
};

export const getTextError = status => {
  switch (status) {
    case 401:
      return "Unauthorized";
    case 403:
      return "Forbidden";
    case 404:
      return "Not Found";
    case 406:
      return "Not Acceptable";

    default:
      return "Unknown";
  }
}

export const getDataManager = urlApi => {
  return (
    new DataManager({
      adaptor: new WebApiAdaptor(),
      url: `${config.URL_API}/${urlApi}`,
      headers: [{ Authorization: "Bearer " + localStorage.getItem('jwt') }]
    })
  );
};

