
import './App.css';
import Navbar from './Components/Navbar';
import Sidebar from './Components/Sidebar';
import Footer from './Components/Footer';
import Layout from './Pages/Layout';
import Index from './Pages/Index';
import About from './Pages/About';
import Contact from './Pages/Contact';
import RSS from './Pages/RSS';
import AdminLayout from './Pages/Admin/Layout';
import * as AdminIndex from './Pages/Admin/Index';
import Authors, * as AdminAuthors from './Pages/Admin/Authors';
import Categories, * as AdminCategories from './Pages/Admin/Categories';
import Comments, * as AdminComments from './Pages/Admin/Comments';
import Posts, * as AdminPosts from './Pages/Admin/Post/Posts';
import Tags, * as AdminTags from './Pages/Admin/Tags';
import NotFound from './Pages/NotFound';
// import BadRequest from './Pages/BadRequest';


import  {
  BrowserRouter as Router,
  RoutesProps,
  Route,
  Routes,
} from 'react-router-dom';




function App() {
  return (
    // <div>
      <Router>
        <Routes>
          <Route path='/' element={<Layout/>}>
            <Route path='/' element={<Index/>}/>
            <Route path='blog' element={<Index/>}/>
            <Route path='blog/Contact' element={<Contact />}/>
            <Route path='blog/About' element={<About />}/>
            <Route path='blog/RSS' element={<RSS/>}/>           
          </Route>
          <Route path='/admin' element={<AdminLayout/>}>
            <Route path='/admin' element={<AdminIndex.default />}/>
            <Route path='/admin/authors' element={<Authors/>}/>
            <Route path='/admin/categories' element={<Categories />}/>
            <Route path='/admin/comments' element={<Comments/>}/>
            <Route path='/admin/posts' element={<Posts />}/>
            <Route path='/admin/tags' element={<Tags />}/>
          </Route>
            {/* <Route path='/400' element={<BadRequest/>}/> */}
            <Route path='*' element={<NotFound/>}/>
        </Routes>
        <Footer/>
      </Router>
    /* </div> */
  );
}


export default App;




// Cập nhật lại code ở trang 3 lab 6
{/* <Navbar/>
<div className='container-fluid'>
  <div className='row'>
    <div className='col-9'>
      <Routes>
        <Route path='/' element={<Layout/>}>
          <Route path='/' element={<Index/>}/>
          <Route path='blog' element={<Index/>}/>
          <Route path='/blog/Contact' element={<Contact/>}/>
          <Route path='/blog/About' element={<About/>}/>
          <Route path='/blog/RSS' element={<RSS/>}/>
        </Route>
      </Routes>
    </div>
    <div className='col-3 border-start'>
      <Sidebar/>  
    </div>
  </div>
</div> */}
