//
// ReactBootstrap Component variables
//
var ListGroup = ReactBootstrap.ListGroup;
var ListGroupItem = ReactBootstrap.ListGroupItem;
var Modal = ReactBootstrap.Modal;

// example of how to do inline style
var rightAlign = {
    textAlign: "right",
    position: "relative",
}

var paddingBottom = {
    paddingBottom: "15px"
}

var centerAlign = {
    textAlign: "center"
}

//
// ModalDetails Component
//
var ModalDetails = React.createClass({
    formatCurrency: function (val) {
        return numeral(val).format('$0,0.00');
    },
    render: function () {
        return (
            <tr>
                <td style={centerAlign}>{this.props.details.Product}</td>
                <td style={rightAlign}>{this.formatCurrency(this.props.details.MSRP)}</td>
                <td style={centerAlign}>{this.props.details.QtyS}</td>
                <td style={centerAlign}>{this.props.details.QtyO}</td>
                <td style={centerAlign}>{this.props.details.QtyB}</td>
                <td style={rightAlign}>{this.formatCurrency(this.props.details.Extended)}</td>
            </tr>
        )
    }
});

//
// Cart Component
//
var Cart = React.createClass({
    getInitialState() {
        return { showModal: false, cartdetails: [] };
    },
    close() {
        this.setState({ showModal: false });
    },
    open() {
        this.setState({ showModal: true });
        var cart = this.props.cart;
        var url = this.props.source + "/" + cart.Id;
        httpGet(url, function (data) {
            this.setState({ cartdetails: data });
        }.bind(this));
    },
    formatCurrency: function (val) {
        return numeral(val).format('$0,0.00');
    },
    render: function () {
        var detailsForModal = this.state.cartdetails.map(details =>
            <ModalDetails details={details} key={details.Id} />
        );
        return (
            <div>
                <ListGroupItem onClick={this.open}>
                    <span className="col-sm-4 col-xs-2" style={centerAlign}>{this.props.cart.Id}</span>
                    <span className="col-sm-8 col-xs-10" style={centerAlign}>{formatDate(this.props.cart.DateCreated)}</span>
                </ListGroupItem>



                <Modal show={this.state.showModal} onHide={this.close}>
                    <Modal.Header closeButton  style={{backgroundColor:"goldenrod"}}>
                        <Modal.Title>
                            <div>
                                <span className="col-xs-12">&nbsp;</span>
                                <span className="col-xs-3 text-center">Cart:{this.props.cart.Id}</span>
                                <span className="col-xs-9 text-right xsmallFont">Date:{formatDate(this.props.cart.DateCreated)}</span>
                            </div>
                        </Modal.Title>
                    </Modal.Header>
                    <Modal.Body  style={{backgroundColor:"goldenrod"}}>
                        <div className="table-responsive">
                            <table className="table">
                                <thead>
                                    <tr>
                                        <th style={centerAlign}>Product</th>
                                        <th style={rightAlign}>MSRP</th>
                                        <th style={centerAlign}>QtyS</th>
                                        <th style={centerAlign}>QtyO</th>
                                        <th style={centerAlign}>QtyB</th>
                                        <th style={rightAlign}>Extended</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {detailsForModal}
                                </tbody>      
                            </table>
                        </div>
                    </Modal.Body>
                    <Modal.Footer  style={{backgroundColor:"goldenrod"}}>
                        <div className="text-right">
                            <span className="col-sm-10 col-xs-9">Subtotal:</span>
                            <span className="col-xs-2">
                                {this.formatCurrency(this.props.cart.Subtotal)}
                            </span>
                            <span className="col-sm-10 col-xs-9">Tax:</span>
                            <span className="col-xs-2">
                                {this.formatCurrency(this.props.cart.Tax)}
                            </span>
                            <span className="col-sm-10 col-xs-9">Order Total:</span>
                            <span className="col-xs-2">
                                {this.formatCurrency(this.props.cart.TotalPrice)}
                            </span>
                        </div>
                    </Modal.Footer>
                </Modal>
            </div>
        )
    }
});


//
// Cartlist Component
//
var Cartlist = React.createClass({
    getInitialState: function () {
        return ({ carts: [] });
    },
    componentDidMount: function () {
        httpGet(this.props.source, function (data) {
            this.setState({ carts: data });
        }.bind(this));
    },
    render: function () {
        var carts = this.state.carts.map(cart =>
            <Cart cart={cart} key={cart.Id} source="/GetCartDetailsAsync" />);
        return (
            <div className="top25">
                <div className="col-sm-4 col-xs-1">&nbsp;</div>
                <div className="panel col-sm-4 col-xs-12"  style={{backgroundColor:"goldenrod"}}>
                    <div className="panel-title text-center" style={{paddingTop:"20px"}}>
                        <h3 style={{fontWeight:"bolder"}}>Order History</h3>
                        <img src="../img/cart.png" width="100" height="100" />
                    </div>
                    <div className="panel-body">
                        <div>
                            <div className="text-center navbar navbar-default" style={{top:"25px",position:"relative"}}>
                                <div className="col-sm-4 col-xs-2" style={{ top: "10px", position: "relative", textAlign: "center" }}>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;#</div>
                                <div className="col-sm-8 col-xs-10" style={{top:"10px",position:"relative", textAlign:"center"}}>Date</div>
                            </div>
                            <ListGroup>
                                {carts}
                            </ListGroup>
                        </div>
                    </div>
                </div>
            </div>
        )
    }
})

ReactDOM.render(
    <Cartlist source="/GetCarts" />, document.getElementById("orderList") // html tag
)