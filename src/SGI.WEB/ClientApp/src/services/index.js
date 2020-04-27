export const getError = (args) => {
  console.log("error ->", args);
  if (args.error && args.error.error) {
    return {
      status: args.error.error.status,
      text: args.error.error.responseText === "" ? "unknown" : args.error.error.responseText
    };
  }

  return { status: -1, text: "unknown" }
};