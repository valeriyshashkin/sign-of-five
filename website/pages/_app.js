function MyApp({ Component, pageProps }) {
  return (
    <>
      <Component {...pageProps} />
      <style global jsx>{`
        body {
          margin: 0;
        }
      `}</style>
    </>
  );
}

export default MyApp;
