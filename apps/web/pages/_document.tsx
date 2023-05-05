import { ColorModeScript } from "@chakra-ui/react"
import Document, {
  Html,
  Head,
  Main,
  NextScript,
  DocumentContext,
  DocumentInitialProps,
} from "next/document"
import { ServerStyleSheet } from "styled-components"
import chakraTheme from "../styles/chakra"

export default class CustomDocument extends Document {
  static async getInitialProps(
    ctx: DocumentContext
  ): Promise<DocumentInitialProps> {
    const originalRenderPage = ctx.renderPage

    const sheet = new ServerStyleSheet()

    ctx.renderPage = () =>
      originalRenderPage({
        enhanceApp: (App) => (props) => sheet.collectStyles(<App {...props} />),
        enhanceComponent: (Component) => Component,
      })

    const intialProps = await Document.getInitialProps(ctx)
    const styles = sheet.getStyleElement()

    return { ...intialProps, styles }
  }

  render() {
    return (
      <Html>
        <Head>{this.props.styles}</Head>
        <body>
          <ColorModeScript
            initialColorMode={chakraTheme.config.initialColorMode}
          />
          <Main />
          <NextScript />
        </body>
      </Html>
    )
  }
}
