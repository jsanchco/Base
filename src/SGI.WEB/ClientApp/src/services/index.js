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